﻿using AutoMapper;
using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Contacts.Queries
{
    public record GetContactsByLocationQuery : IRequest<List<ContactDto>>
    {
        public string? Location { get; set; }
    }

    public class GetContactsByLocationQueryHandler : IRequestHandler<GetContactsByLocationQuery, List<ContactDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContactsByLocationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> Handle(GetContactsByLocationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Contacts
                .Include(p => p.Person)
                .Where(p => p.Location == request.Location)
                .OrderBy(x => x.Person.Name)
                .Select(p => new ContactDto()
                {
                    Name = p.Person.Name,
                    Surname = p.Person.Surname,
                    Email = p.Email,
                    Id = p.Id,
                    Location = p.Location,
                    PhoneNumber = p.PhoneNumber,
                    PersonId = p.PersonId
                }).ToListAsync();
        }
    }
}