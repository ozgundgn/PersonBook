
using ICG.NetCore.Utilities.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ServiceConnectUtils;
using ServiceConnectUtils.BaseModels;
using ServiceConnectUtils.Enums;

namespace ReportService.Controllers
{
    public class ReportsController : ApiControllerBase
    {

        public ReportsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<GeneralResponse<Guid>> Create(CreateReportCommand command)
        {
            command.Uuid = Guid.NewGuid();

            var result= new GeneralResponse<Guid>()
            {
                Object = await _mediator.Send(command),
            };

            ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/preparereport", command);
            return result;
        }

        [HttpPost("getall")]
        public async Task<GeneralResponse<IEnumerable<ReportDto>>> GetAll(GetAllReportsQuery command)
        {
            return new GeneralResponse<IEnumerable<ReportDto>>()
            {
                Object = await _mediator.Send(command)
            };
        }

        [HttpPost("delete")]
        public async Task<GeneralResponse> Delete(DeleteReportCommand command)
        {
            await _mediator.Send(command);
            return new GeneralResponse();
        }

        [HttpPost("update")]
        public async Task<GeneralResponse> Update(UpdateReportCommand command)
        {
            await _mediator.Send(command);
            return new GeneralResponse();
        }


        [HttpPost("getbylocation")]
        public async Task<GeneralResponse<List<ReportDto>>> GetReportsByLocationQuery(GetReportsByLocationQuery command)
        {
            var result = new GeneralResponse<List<ReportDto>>();
            if (string.IsNullOrEmpty(command.Location))
            {
                result.Success = false;
                result.Message = "Location is empty.";

            }
            result.Object = await _mediator.Send(command);
            return result;
        }
    }
}
