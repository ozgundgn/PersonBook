using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Contacts.Queries
{
    public record PrepareReportCommand : IRequest
    {
        public string Location { get; set; }
        public int PersonsCount { get; set; }
        public int PhoneNumbersCount { get; set; }
        public Guid Uuid { get; set; }
    }

    public class PrepareReportCommandHandler : IRequestHandler<PrepareReportCommand>
    {
        private readonly IApplicationDbContext _context;

        public PrepareReportCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PrepareReportCommand request, CancellationToken cancellationToken)
        {
            var list = await _context.Contacts
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
                }) .ToListAsync();
            return Unit.Value;
        }
    }
}
