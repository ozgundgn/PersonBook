using AutoMapper;
using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceConnectUtils.BaseModels;

namespace ContactService.Application.Contacts.Queries
{
    public record GetContactsByLocationQuery : IRequest<List<ContactDto>>, IReturn<GeneralResponse<List<ContactDto>>>
    {
        public string? Location { get; set; }
    }

    public class GetContactsByLocationQueryHandler : IRequestHandler<GetContactsByLocationQuery, List<ContactDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetContactsByLocationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
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
