using AutoMapper;
using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Contacts.Commands
{
    public record UpdateContactCommand : IRequest
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int PersonId { get; set; }
    }
    public class UpdateContactComandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateContactComandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentException("There's nothing to update");

            var entity = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new ArgumentException(string.Format("There's nothing to update for id:{0}", request.Id));

            entity.Location = request.Location;
            entity.PhoneNumber = request.PhoneNumber;
            entity.Email = request.Email;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
