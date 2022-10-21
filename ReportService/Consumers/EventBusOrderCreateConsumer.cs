using AutoMapper;
using EventRabbitMQ;
using EventRabbitMQ.Core;
using EventRabbitMQ.Events;
using ICG.NetCore.Utilities.Spreadsheet;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ServiceConnectUtils;
using ServiceConnectUtils.Enums;
using System.Text;

namespace ReportService.Consumers
{
    public class EventBusReportCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IMapper _mapper;
        private readonly IMediator _imediator;
        private ISpreadsheetGenerator _spreadsheetGenerator;

        public EventBusReportCreateConsumer(IRabbitMQPersistentConnection persistentConnection, IMapper mapper, IMediator imediator, ISpreadsheetGenerator spreadsheetGenerator)
        {
            _persistentConnection = persistentConnection;
            _mapper = mapper;
            _imediator = imediator;
            _spreadsheetGenerator = spreadsheetGenerator;
        }

        public void Consume()
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var channel = _persistentConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.ReportCompletedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.ReportCompletedQueue, autoAck: true, consumer: consumer);

        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<ReportCreatedEvent>(message);

            if (e.RoutingKey == EventBusConstants.ReportCompletedQueue)
            {
                var command = new UpdateReportCommand()
                {
                    Path = @event.Location,
                    Uuid = @event.Uuid
                };

                ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/update", command);


                //await _imediator.Send(command);


                var list = new List<CreateReportExport>()
                {
                new CreateReportExport(){Path=command.Path,Uuid=command.Uuid,PersonsCount=@event.PersonsCount,PhonesCount=@event.PhoneNumbersCount}
                };

                string root = @"C:\POCReports";
                // If directory does not exist, create it.
                Directory.CreateDirectory(root);

                var exportDefinition = new SpreadsheetConfiguration<CreateReportExport>
                {
                    RenderTitle = true,
                    DocumentTitle = string.Format("Location:{0} Persons and Phones Numbers", command.Path),
                    RenderSubTitle = true,
                    DocumentSubTitle = "Showing the full options",
                    ExportData = list,
                    WorksheetName = command.Uuid.ToString(),
                };
                var fileContent = _spreadsheetGenerator.CreateSingleSheetSpreadsheet(exportDefinition);
                System.IO.File.WriteAllBytes(String.Concat(root + @"\" + command.Uuid, ".xlsx"), fileContent);

            }
        }
    }
}
