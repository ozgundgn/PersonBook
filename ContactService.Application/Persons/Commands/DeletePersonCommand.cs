﻿using ContactService.Application.Common.Interfaces;
using MediatR;

namespace ContactService.Application.Persons.Commands
{
    public record DeletePersonCommand(int Id) : IRequest;
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Persons
           .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                throw new ArgumentException("There's nothing to delete");


            _context.Persons.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
