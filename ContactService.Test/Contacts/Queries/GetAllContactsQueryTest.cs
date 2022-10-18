using AutoMapper;
using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Contacts.Queries;
using ContactService.Test.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity.Infrastructure;
using Xunit;
namespace ContactService.Test.Commands
{
    public class GetAllContactsQueryTest
    {
        private  Mock<ContactDbContext> _mockContext;
        private  Mock<DbSet<Contact>> _mockSetContact;

        private GetAllContactsQueryHandler _getAllContactsCommandHandler;

        public GetAllContactsQueryTest()
        {
            _mockContext = new Mock<ContactDbContext>();
            _mockSetContact = new Mock<DbSet<Contact>>();
        }

        [Theory]
        [ClassData(typeof(GetAllContactsQueryTestData))]
        public async Task GetAllContactsCommand_SimpleDataSend_ReturnsEquals(GetAllContactsQuery contact)
        {

            var data = new List<Contact>
            {
                new Contact {  Id = 1,
                Email = "testupdat1e@gmail.com",
                PersonId = 3,
                Location = "Kocaeli",
                PhoneNumber = "032254788965",Person=new Person{Company="deneme2",Name="Aylin",Surname="Hayat"} },
                 new Contact {  Id = 2,
                Email = "testupdate2@gmail.com",
                PersonId = 2,
                Location = "Urfa",
                PhoneNumber = "022254788965" ,Person=new Person{Company="deneme3",Name="Güneþ",Surname="Ateþ"}},
                 new Contact {  Id = 3,
                Email = "testupdate3@gmail.com",
                PersonId = 2,
                Location = "Kýrklareli",
                PhoneNumber = "012254788965",Person=new Person{Company="deneme",Name="Hasan",Surname="Ali"}
                },
            }.AsQueryable();

            _mockSetContact.As<IDbAsyncEnumerable<Contact>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Contact>(data.GetEnumerator()));

            _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSetContact.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());


            _mockContext.Setup(m => m.Contacts).Returns(_mockSetContact.Object);

            _getAllContactsCommandHandler = new GetAllContactsQueryHandler(_mockContext.Object);


            var result = await _getAllContactsCommandHandler.Handle(contact, new CancellationToken());
            Assert.IsType<List<ContactDto>>(result);
        }

    }
}