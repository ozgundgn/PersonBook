using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Common.Interfaces;
using ReportService.Domain.Entities;

namespace ReportService.Application.Reports.Commands
{
    public class CreateReportCommand : IRequest<Guid>
    {
        public string Location { get; set; }
        public Guid Uuid { get; set; }
    }
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Guid>
    {
        private readonly IApplicationReportDbContext _context;
        private readonly IMapper _mapper;

        public CreateReportCommandHandler(IApplicationReportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Report>(request);
            if (entity == null)
                throw new ArgumentException("Could not be mapped with null object");

            entity.Uuid = request.Uuid;
            entity.State = 0;
            entity.Path = @"C:\POCReports\" + request.Uuid.ToString();
            entity.CreatedDate = DateTime.Now;
            var responseEntity = _context.Reports.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Uuid;
        }
    }
}
