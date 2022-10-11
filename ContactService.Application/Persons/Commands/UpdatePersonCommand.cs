using AutoMapper;
using ContactService.Application.Common.Interfaces;
using MediatR;

namespace ContactService.Application.Persons.Commands
{
    public record UpdatePersonCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Company { get; set; }
    }
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePersonCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Persons
           .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new ArgumentException(string.Format("There's nothing to update for id:{0}", request.Id));
            }

            entity.Name = request.Name;
            entity.Surname = request.Surname;
            entity.Company = request.Company;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
