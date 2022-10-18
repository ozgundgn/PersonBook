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
    public class UpdatePersonCommandTest
    {
        private  Mock<ContactDbContext> _mockContext;
        private  Mock<DbSet<Person>> _mockSetPerson;
        private UpdatePersonCommandHandler _updatePersonCommandHandler;
        public UpdatePersonCommandTest()
        {
            _mockContext = new Mock<ContactDbContext>();
            _mockSetPerson = new Mock<DbSet<Person>>();
            _updatePersonCommandHandler = new UpdatePersonCommandHandler(_mockContext.Object);

        }

        [Theory]
        [ClassData(typeof(UpdatePersonCommandTestData))]
        public async Task UpdatePersonCommand_SimpleDataUpdate_ReturnsEqualsVoid(UpdatePersonCommand person)
        {

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
            },
                              new Person {
                    Id = 3,
                Name = "DenemeName3",
                Surname = "DenemeSurname3",
                Company = "Rise3"
            }

            };

            
            var personMock = dataPersons.AsQueryable().BuildMockDbSet();

    
            _mockContext.Setup(m => m.Persons).Returns(personMock.Object);

            var _updatePersonCommandHandler = new UpdatePersonCommandHandler(_mockContext.Object);
            //Arrange

           
            // Act
            var result = await _updatePersonCommandHandler.Handle(person, new CancellationToken());
            // Assert
            Assert.IsType<Unit>(result);
        }

        [Fact]
        public async Task UpdatePersonCommand_NullDataSend_ReturnsEqualsArgumentException()
        {
           
            await Assert.ThrowsAsync<ArgumentException>(async () => await _updatePersonCommandHandler.Handle(null, new CancellationToken()));
        }

    }
}