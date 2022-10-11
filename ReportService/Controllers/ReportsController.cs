
using ICG.NetCore.Utilities.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ServiceConnectUtils;
using ServiceConnectUtils.Enums;

namespace ReportService.Controllers
{
    public class ReportsController : ApiControllerBase
    {
       
        public ReportsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create(CreateReportCommand command)
        {
            command.Uuid = Guid.NewGuid();
            var uuid = await _mediator.Send(command);
            ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/preparereport", HttpMethod.Post, command);

            return Ok(uuid);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllReportsQuery());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteReportCommand(id));
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update( UpdateReportCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("getbylocation")]
        public async Task<ActionResult<List<ReportDto>>> GetReportsByLocationQuery(GetReportsByLocationQuery command)
        {
            if (string.IsNullOrEmpty(command.Location))
                return BadRequest();

            return await _mediator.Send(command);

        }
    }
}
