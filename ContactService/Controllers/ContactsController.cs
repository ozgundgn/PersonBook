using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using EventBusRabbitMQ.Producer;
using EventRabbitMQ.Core;
using EventRabbitMQ.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<int>> Create(CreateContactCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllContactsQuery());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteContactCommand(id));
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(UpdateContactCommand command)
        {
            if (command.Id <= 0)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("getbylocation")]
        public async Task<ActionResult<List<ContactDto>>> GetContactsByLocationQuery(GetContactsByLocationQuery command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("preparereport")]

        public async Task<ActionResult> PrepareReport(PrepareReportCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Location))
                return BadRequest();

            var contactsByLocation = await _mediator.Send(new GetContactsByLocationQuery()
            {
                Location = command.Location,
            });

            ReportCreatedEvent reportMessage = new ReportCreatedEvent()
            {
                Uuid = command.Uuid,
                PhoneNumbersCount = contactsByLocation.Count,
                PersonsCount = contactsByLocation.GroupBy(x => x.PersonId).Count(),
                Location=command.Location
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
            return Ok();

        }
    }
}