using ContactService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceConnectUtils.BaseModels;

namespace ContactService.Application.Contacts.Commands
{
    public record DeleteContactCommand(int Id) : IRequest,IReturn<GeneralResponse>;
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteContactCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            if(request==null)
                throw new ArgumentException("There's nothing to delete");

            var entity = await _context.Contacts.FirstOrDefaultAsync(x=>x.Id==request.Id);

            if (entity == null)
                throw new ArgumentException("There's nothing to delete");


            _context.Contacts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
