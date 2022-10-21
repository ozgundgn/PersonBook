using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using EventBusRabbitMQ.Producer;
using EventRabbitMQ.Core;
using EventRabbitMQ.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceConnectUtils.BaseModels;

namespace ContactService.Controllers
{

    public class ContactsController : ApiControllerBase
    {
        private EventBusRabbitMQProducer _rabbitMq;
        private readonly ILogger<ContactsController> _logger;
        public ContactsController(IMediator mediator, EventBusRabbitMQProducer rabbitMq, ILogger<ContactsController> logger) : base(mediator)
        {
            _rabbitMq = rabbitMq;
            _logger = logger;
        }
        [HttpPost("create")]
        public async Task<GeneralResponse<int>> Create(CreateContactCommand command)
        {
            return new GeneralResponse<int>
            {
                Object = await _mediator.Send(command)
            };
        }

        [HttpPost("getall")]
        public async Task<GeneralResponse<IEnumerable<ContactDto>>> GetAll(GetAllContactsQuery request)
        {
            return new GeneralResponse<IEnumerable<ContactDto>>
            {
                Object = await _mediator.Send(request)
            };
        }

        [HttpPost("delete")]
        public async Task<GeneralResponse> Delete(DeleteContactCommand request)
        {
            var result = new GeneralResponse();
            if (request.Id <= 0)
            {
                result.Success = false;
                result.Message = "Invalid contact id";
            }

            await _mediator.Send(request);
            return result;
        }

        [HttpPost("update")]
        public async Task<GeneralResponse> Update(UpdateContactCommand request)
        {
            var result = new GeneralResponse();

            if (request.Id <= 0)
            {
                result.Success = false;
                result.Message = "Invalid contact id";
            }
            await _mediator.Send(request);

            return result;
        }


        [HttpPost("getbylocation")]
        public async Task<GeneralResponse<List<ContactDto>>> GetContactsByLocationQuery(GetContactsByLocationQuery request)
        {
            return new GeneralResponse<List<ContactDto>>
            {
                Object = await _mediator.Send(request)
            };
        }

        [HttpPost("preparereport")]

        public async Task<GeneralResponse> PrepareReport(PrepareReportCommand request)
        {
            var result = new GeneralResponse();
            if (string.IsNullOrWhiteSpace(request.Location))
            {
                result.Success = false;
                result.Message = "There is no location to report.";

            }

            var contactsByLocation = await _mediator.Send(new GetContactsByLocationQuery()
            {
                Location = request.Location,
            });

            ReportCreatedEvent reportMessage = new ReportCreatedEvent()
            {
                Uuid = request.Uuid,
                PhoneNumbersCount = contactsByLocation.Count,
                PersonsCount = contactsByLocation.GroupBy(x => x.PersonId).Count(),
                Location = request.Location
            };
            try
            {
                _rabbitMq.Publish(EventBusConstants.ReportCompletedQueue, reportMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error publishing event: {EventId} from {AppName}", reportMessage.RequestId, "ContactsController");

                throw;
            }
            return result;

        }
    }
}