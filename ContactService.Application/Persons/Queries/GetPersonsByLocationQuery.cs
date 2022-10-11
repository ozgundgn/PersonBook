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
    public record GetPersonsByLocationQuery : IRequest<List<PersonDto>>
    {
        public string? Location { get; set; }
    }

    public class GetPersonsByLocationQueryHandler : IRequestHandler<GetPersonsByLocationQuery, List<PersonDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPersonsByLocationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PersonDto>> Handle(GetPersonsByLocationQuery request, CancellationToken cancellationToken)
        {
             return await _context.Persons
                .Where(p=>p.Contacts.Any())
                .Include( p =>p.Contacts.Where(c=>c.Location==c.Location))
                .OrderBy(x => x.Name)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .ToListAsync<PersonDto>();
        }
    }
}
