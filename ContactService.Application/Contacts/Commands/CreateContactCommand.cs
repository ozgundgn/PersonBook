using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceConnectUtils.BaseModels;

namespace ContactService.Application.Contacts.Commands
{
    public class CreateContactCommand : IRequest<int>, IReturn<GeneralResponse<int>>
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int PersonId { get; set; }
        public int Id { get; set; }
    }
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Contact>(request);
            if (entity == null)
                throw new ArgumentException("Could not be mapped with null object");


            var responseEntity = _context.Contacts.Add(entity);
            var id = _context.SaveChangesAsync(cancellationToken);
            return id.Id;
        }
    }
}
