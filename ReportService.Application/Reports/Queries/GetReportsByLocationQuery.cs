using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Common.Interfaces;

namespace ReportService.Application.Reports.Queries
{
    public record GetReportsByLocationQuery : IRequest<List<ReportDto>>
    {
        public string? Location { get; set; }
    }

    public class GetReportsByLocationQueryHandler : IRequestHandler<GetReportsByLocationQuery, List<ReportDto>>
    {
        private readonly IApplicationReportDbContext _context;
        private readonly IMapper _mapper;

        public GetReportsByLocationQueryHandler(IApplicationReportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReportDto>> Handle(GetReportsByLocationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reports
               .Where(p => p.Path == request.Location)
               .ProjectTo<ReportDto>(_mapper.ConfigurationProvider)
               .ToListAsync();
        }
    }
}
