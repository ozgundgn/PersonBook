using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Common.Interfaces;

namespace ReportService.Application.Reports.Queries
{
    public record GetAllReportsQuery : IRequest<List<ReportDto>>
    {

    }

    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, List<ReportDto>>
    {
        private readonly IApplicationReportDbContext _context;
        private readonly IMapper _mapper;

        public GetAllReportsQueryHandler(IApplicationReportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReportDto>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reports
                .OrderBy(x => x.Id)
                .ProjectTo<ReportDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
