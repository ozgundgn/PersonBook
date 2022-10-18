using AutoMapper;
using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Contacts.Queries;
using ContactService.Test.Models;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Data.Entity.Infrastructure;
using Xunit;
namespace ContactService.Test.Commands
{
    public class GetAllContactsQueryTest
    {
        private  Mock<ContactDbContext> _mockContext;
        private  Mock<DbSet<Contact>> _mockSetContact;
        private Mock<DbSet<Person>> _mockSetPerson;


        private GetAllContactsQueryHandler _getAllContactsCommandHandler;

        public GetAllContactsQueryTest()
        {
            _mockContext = new Mock<ContactDbContext>();
            _mockSetContact = new Mock<DbSet<Contact>>();
            _mockSetPerson = new Mock<DbSet<Person>>();
        }

        [Theory]
        [ClassData(typeof(GetAllContactsQueryTestData))]
        public async Task GetAllContactsCommand_SimpleDataSend_ReturnsEquals(GetAllContactsQuery contact)
        {

            var dataContacts = new List<Contact>
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
            };

            var contactMock = dataContacts.AsQueryable().BuildMockDbSet();

            var dataPersons = new List<Person>
            {
               new Person{Id=1,Company="deneme2",Name="Aylin",Surname="Hayat"},
               new Person{Id=2,Company="deneme",Name="Hasan",Surname="Ali"},
               new Person{Id=3,Company="deneme3",Name="Güneþ",Surname="Ateþ"}
            };

            var personsMock= dataPersons.AsQueryable().BuildMockDbSet();




            _mockContext.Setup(m => m.Contacts).Returns(contactMock.Object);
            _mockContext.Setup(m => m.Persons).Returns(personsMock.Object);
            var userId = 1;

            //contactMock.Setup(x => x.FindAsync(userId)).ReturnsAsync((int[] ids) =>
            //{
            //    var id = ids[0];
            //    return dataContacts.Find(x=>x.Id==id);
            //});

            _getAllContactsCommandHandler = new GetAllContactsQueryHandler(_mockContext.Object);


            var result = await _getAllContactsCommandHandler.Handle(contact, new CancellationToken());
            Assert.IsType<List<ContactDto>>(result);
        }

    }
}