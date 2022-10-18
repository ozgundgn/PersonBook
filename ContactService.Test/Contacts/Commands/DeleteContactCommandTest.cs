using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Application.Contacts.Commands;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Contacts.Queries;
using ContactService.Test.Models;
using FakeItEasy.Sdk;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Data.Entity.Infrastructure;
using Xunit;
namespace ContactService.Test.Commands
{
    public class DeleteContactCommandTest
    {
        //private readonly Mock<IRequestHandler<CreateContactCommand,int>> _createContactCommandHandlerMock;
        private Mock<ContactDbContext> _mockContext;
        private Mock<DbSet<Contact>> _mockSetContact;

        private readonly DeleteContactCommandHandler _deleteContactCommandHandler;

        public DeleteContactCommandTest()
        {
            _mockSetContact = new Mock<DbSet<Contact>>();
            _mockContext = new Mock<ContactDbContext>();
            // Init
            _deleteContactCommandHandler = new DeleteContactCommandHandler(_mockContext.Object);
        }

        [Theory]
        [ClassData(typeof(DeleteContactCommandTestData))]
        public async Task DeleteContactCommand_SimpleDataDelete_ReturnsEquals(DeleteContactCommand contact)
        {
            //Arrange

            var dataContacts = new List<Contact>
            {
                new Contact {  Id = 1,
                Email = "testupdat1e@gmail.com",
                PersonId = 3,
                Location = "Kocaeli",
                PhoneNumber = "032254788965" }
            };

            var contactMock = dataContacts.AsQueryable().BuildMockDbSet();


        //    _mockSetContact.As<IDbAsyncEnumerable<Contact>>()
        //.Setup(m => m.GetAsyncEnumerator())
        //.Returns(new TestDbAsyncEnumerator<Contact>(data.GetEnumerator()));

        //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
        //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            _mockContext.Setup(m => m.Contacts).Returns(contactMock.Object);

            // Act
            var result = await _deleteContactCommandHandler.Handle(contact, new CancellationToken());

            // Assert
            Assert.IsType<Unit>(result);
        }
        [Fact]
        public async Task DeleteContactCommand_NullDataSend_ReturnsEqualsArgumentException()
        {

            _mockContext.Setup(m => m.Contacts).Returns(_mockSetContact.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _deleteContactCommandHandler.Handle(null, new CancellationToken()));
        }

    }
}