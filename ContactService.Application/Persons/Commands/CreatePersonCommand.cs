using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceConnectUtils.BaseModels;

namespace ContactService.Application.Persons.Commands
{
    public class CreatePersonCommand : IRequest<Guid>, IReturn<GeneralResponse<Guid>>
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Company { get; set; }
    }
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePersonCommandHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Person>(request);
            if (entity == null)
                throw new ArgumentException("Could not be mapped with null object");

            entity.Uuid = Guid.NewGuid();
            var responseEntity = _context.Persons.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Uuid;
        }
    }
}
