using AutoMapper;
using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Contacts.Queries
{
    public record GetAllContactsQuery : IRequest<List<ContactDto>>
    {

    }

    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, List<ContactDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllContactsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {

            return await _context.Contacts
             .OrderBy(x => x.Email)
             .Select(x => new ContactDto
             {
                 Email = x.Email,
                 Name = x.Person.Name,
                 Surname = x.Person.Surname,
                 Location = x.Location,
                 PhoneNumber = x.PhoneNumber,
                 PersonId = x.PersonId,
                 Id = x.Id
             })
             .ToListAsync();


        }
    }
}
