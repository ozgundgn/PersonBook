using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Application.Contacts.Commands;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Models;
using FakeItEasy.Sdk;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
namespace ContactService.Test.Contacts.Commands
{
    public class CreatelContactsQueryTest
    {
        //private readonly Mock<IRequestHandler<CreateContactCommand,int>> _createContactCommandHandlerMock;
        private Mock<ContactDbContext> _mockContext;
        private Mock<DbSet<Contact>> _mockSetContact;

        private readonly CreateContactCommandHandler _createContactCommandHandler;

        public CreatelContactsQueryTest()
        {
            var mapper = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<Contact, CreateContactCommand>().ReverseMap();
           }).CreateMapper();
            _mockSetContact = new Mock<DbSet<Contact>>();
            _mockContext = new Mock<ContactDbContext>();
            _mockContext.Setup(m => m.Contacts).Returns(_mockSetContact.Object);
            // Init
            _createContactCommandHandler = new CreateContactCommandHandler(_mockContext.Object, mapper);
        }

        [Theory]
        [ClassData(typeof(CreateContactCommandTestData))]
        public async Task CreateContactCommand_SimpleDataSend_ReturnsEquals(CreateContactCommand contact)
        {
            //Arrange
            

            // Act
            var result = await _createContactCommandHandler.Handle(contact, new CancellationToken());

            // Assert
            Assert.IsType<int>(result);
        }
        [Fact]
        public async Task CreateContactCommand_NullDataSend_ReturnsEqualsArgumentException()
        {

            await Assert.ThrowsAsync<ArgumentException>(async () => await _createContactCommandHandler.Handle(null, new CancellationToken()));
        }

    }
}