using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Common.Interfaces;

namespace ReportService.Application.Reports.Commands
{
    public record UpdateReportCommand : IRequest
    {
        public string Path { get; set; }
        public Guid Uuid { get; set; }
    }
    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand>
    {
        private readonly IApplicationReportDbContext _context;

        public UpdateReportCommandHandler(IApplicationReportDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Reports.FirstOrDefaultAsync(x => x.Uuid == request.Uuid);

            if (entity == null)
            {
                throw new ArgumentException(string.Format("There's nothing to update for uuid:{0}", request.Uuid));
            }

           entity.CreatedDate = DateTime.Now;
            entity.State = 1;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
