using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Common.Interfaces;

namespace ReportService.Application.Reports.Commands
{
    public record DeleteReportCommand(int Id) : IRequest;
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand>
    {
        private readonly IApplicationReportDbContext _context;

        public DeleteReportCommandHandler(IApplicationReportDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentException("There's nothing to delete.");

            var entity = await _context.Reports
           .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new ArgumentException("There's nothing to delete");


            _context.Reports.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
