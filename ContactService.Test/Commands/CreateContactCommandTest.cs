using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Application.Contacts.Commands;
using ContactService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
namespace ContactService.Test.Commands
{
    public class CreateContactCommandTest
    {
        private readonly Mock<IRequestHandler<CreateContactCommand,int>> _createContactCommandHandlerMock;
        private readonly Mock<IApplicationDbContext> _context;
        private readonly CreateContactCommandHandler _createContactCommandHandler;

        public CreateContactCommandTest()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Contact, CreateContactCommand>().ReverseMap();
            }).CreateMapper();

            _context = new Mock<IApplicationDbContext>();
            // Init
            _createContactCommandHandler = new CreateContactCommandHandler( _context.Object, mapper);
        }

        [Fact]
        public async Task CreateContactCommand_SimpleDataSend_ReturnsEquals()
        {
            //Arrange
            var model = new CreateContactCommand()
            {
                Id = 1,
                PersonId = 1,
                Email = "dddd@aaa.com",
                Location = "",
                PhoneNumber = "00000000000"
            };

            var myDbContextMock = new Mock<DbContext>();
            myDbContextMock.Setup(x => x.Add(It.IsAny<Contact>)).Returns(() =>
            {
                return new Contact();
            });

            myDbContextMock.Setup(x => x.SaveChangesAsync(new CancellationToken()));


            // Act
            var result = await _createContactCommandHandler.Handle(model, new CancellationToken());

            // Assert
            Assert.NotNull(result);

        }
    }
}