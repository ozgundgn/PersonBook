using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Application.Contacts.Commands;
using ContactService.Application.Persons.Commands;
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
namespace ContactService.Test.Persons.Commands
{
    public class DeletePersonCommandTest
    {
        //private readonly Mock<IRequestHandler<CreateContactCommand,int>> _createContactCommandHandlerMock;
        private Mock<ContactDbContext> _mockContext;
        private Mock<DbSet<Person>> _mockSetPerson;
        private DeletePersonCommandHandler _deletePersonCommandHandler;

        public DeletePersonCommandTest()
        {
            _mockSetPerson = new Mock<DbSet<Person>>();
            _mockContext = new Mock<ContactDbContext>();
            // Init
            _deletePersonCommandHandler = new DeletePersonCommandHandler(_mockContext.Object);
        }

        [Theory]
        [ClassData(typeof(DeletePersonCommandTestData))]
        public async Task DeletePersonCommand_SimpleDataDelete_ReturnsEquals(DeletePersonCommand contact)
        {
            //Arrange

            var dataPersons = new List<Person>
            {
                new Person {
                    Id = 1,
                Name = "DenemeName1",
                Surname = "DenemeSurname1",
                Company = "Rise1"
            },
                     new Person {
                    Id = 2,
                Name = "DenemeName2",
                Surname = "DenemeSurname2",
                Company = "Rise2"
            }
            };

            var personMock = dataPersons.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Persons).Returns(personMock.Object);

            // Act
            var result = await _deletePersonCommandHandler.Handle(contact, new CancellationToken());

            // Assert
            Assert.IsType<Unit>(result);
        }
        [Fact]
        public async Task DeletePersonCommand_NullDataSend_ReturnsEqualsArgumentException()
        {

            _mockContext.Setup(m => m.Persons).Returns(_mockSetPerson.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await _deletePersonCommandHandler.Handle(null, new CancellationToken()));
        }

    }
}