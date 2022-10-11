using EventBusRabbitMQ.Producer;
using EventRabbitMQ;
using ReportService.Consumers;

namespace ReportService.BackgroundWorker
{
    public class ApiWorker : BackgroundService
    {
        public ILogger _loger;
        private EventBusReportCreateConsumer _rabbitMq;
        public ApiWorker(ILogger<ApiWorker> loger, EventBusReportCreateConsumer rabbitMq)
        {
            _loger = loger;
            _rabbitMq = rabbitMq;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _loger.LogInformation("Bende Seni çoooooooooooooooooook Seviyorum");
            _rabbitMq.Consume();
            return Task.CompletedTask;
        }
    }
}