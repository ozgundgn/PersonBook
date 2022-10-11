using ContactService.Application.Common.Interfaces;
using MediatR;


namespace ContactService.Application.Contacts.Commands
{
    public record DeleteContactCommand(int Id) : IRequest;
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contacts
           .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                throw new ArgumentException("There's nothing to delete");


            _context.Contacts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
