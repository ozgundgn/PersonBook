using ContactService.Application.Contacts.Queries;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceConnectUtils.BaseModels;

namespace ContactService.Controllers
{
    public class PersonsController : ApiControllerBase
    {
        public PersonsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<Guid>> Create(CreatePersonCommand command)
        {
            return new GeneralResponse<Guid>()
            {
                Object = await _mediator.Send(command)
            };
        }

        [HttpPost("getall")]
        public async Task<GeneralResponse<IEnumerable<PersonDto>>> GetAll(GetAllPersonsQuery request)
        {
            return new GeneralResponse<IEnumerable<PersonDto>>
            {
                Object = await _mediator.Send(request)
            };
        }

        [HttpPost("delete")]
        public async Task<GeneralResponse> Delete(DeletePersonCommand request)
        {
            await _mediator.Send(request);
            return new GeneralResponse { };
        }

        [HttpPost("update")]
        public async Task<GeneralResponse> Update(UpdatePersonCommand command)
        {
            var result = new GeneralResponse();
            if (command.Id <= 0)
            {
                result.Success = false;
                result.Message = "Person has invalid value of id";
            }
            await _mediator.Send(command);
            return result;
        }


        [HttpPost("getbylocation")]
        public async Task<GeneralResponse<List<PersonDto>>> GetPersonsByLocationQuery(GetPersonsByLocationQuery command)
        {
            var result = new GeneralResponse<List<PersonDto>>();

            if (string.IsNullOrEmpty(command.Location))
            {
                result.Success = false;
                result.Message = "Person has invalid value of location";
            }

            result.Object= await _mediator.Send(command); 

            return result;

        }
    }
}
