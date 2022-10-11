using ContactService.Application.Contacts.Queries;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Controllers
{
    public class PersonsController : ApiControllerBase
    {
        public PersonsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create(CreatePersonCommand command)
        {
            
            return await _mediator.Send(command);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllPersonsQuery());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePersonCommand(id));
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update( UpdatePersonCommand command)
        {
            if (command.Id <= 0)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("getbylocation")]
        public async Task<ActionResult<List<PersonDto>>> GetPersonsByLocationQuery(GetPersonsByLocationQuery command)
        {
            if (string.IsNullOrEmpty(command.Location))
                return BadRequest();

            return await _mediator.Send(command);

        }
    }
}
