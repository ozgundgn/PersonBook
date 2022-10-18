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

namespace ContactService.Test.Contacts.Commands
{
    public class UpdateContactCommandTest
    {
        private readonly Mock<ContactDbContext> _mockContext;
        private new Mock<DbSet<Contact>> _mockSetContact;
        public UpdateContactCommandTest()
        {
            _mockContext = new Mock<ContactDbContext>();
            _mockSetContact = new Mock<DbSet<Contact>>();
        }

        [Fact]
        public async Task UpdateContactCommand_SimpleDataUpdate_ReturnsEqualsVoid()
        {

            var dataContacts = new List<Contact>
            {
                new Contact {  Id = 1,
                Email = "testupdat1e@gmail.com",
                PersonId = 3,
                Location = "Kocaeli",
                PhoneNumber = "032254788965",Person=new Person{Company="deneme2",Name="Aylin",Surname="Haklý" }
            } };

            
            var contactMock = dataContacts.AsQueryable().BuildMockDbSet();

    
            _mockContext.Setup(m => m.Contacts).Returns(contactMock.Object);

            //    _mockSetContact.As<IDbAsyncEnumerable<Contact>>()
            //.Setup(m => m.GetAsyncEnumerator())
            //.Returns(new TestDbAsyncEnumerator<Contact>(data.GetEnumerator()));

            //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //    _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            //var id = 1;
            //contactMock.Setup(x => x.FindAsync(id)).ReturnsAsync((object[] ids) =>
            //{
            //    var id = (int)ids[0];
            //    return dataContacts.Find(x => x.Id == id);
            //});


            var _updateContactCommandHandler = new UpdateContactComandHandler(_mockContext.Object);
            //Arrange

            var updtaeContact = new UpdateContactCommand()
            {
                Id = 1,
                Email = "testupdate@gmail.com",
                PersonId = 3,
                Location = "Denmark",
                PhoneNumber = "022254788965"
            };
            // Act
            var result = await _updateContactCommandHandler.Handle(updtaeContact, new CancellationToken());
            // Assert
            Assert.IsType<Unit>(result);
        }

        [Fact]
        public async Task UpdateContactCommand_NullDataSend_ReturnsEqualsArgumentException()
        {
            var _updateContactCommandHandler = new UpdateContactComandHandler(_mockContext.Object);
            await Assert.ThrowsAsync<ArgumentException>(async () => await _updateContactCommandHandler.Handle(null, new CancellationToken()));
        }

    }
}