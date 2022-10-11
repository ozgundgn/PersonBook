using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        public IMediator _mediator = null!;
        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
