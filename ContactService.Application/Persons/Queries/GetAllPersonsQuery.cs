using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Persons.Queries
{
    public record GetAllPersonsQuery : IRequest<List<PersonDto>>
    {

    }

    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<PersonDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPersonsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Persons
                .OrderBy(x => x.Name)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .ToListAsync<PersonDto>();
        }
    }
}
