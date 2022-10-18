using AutoMapper;
using ContactService.Application.Contacts.Queries;
using ContactService.Application.Persons.Queries;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Models;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Data.Entity.Infrastructure;
using Xunit;

namespace ContactService.Test.Persons.Queries
{
    public class GetPersonsByLocationQueryTest
    {
        private Mock<ContactDbContext> _mockContext;
        private Mock<DbSet<Person>> _mockSetPerson;
        private IMapper _mapper;



        private GetPersonsByLocationQueryHandler _getByLocationReportCommandHandler;

        public GetPersonsByLocationQueryTest()
        {
            _mockContext = new Mock<ContactDbContext>();
            _mockSetPerson = new Mock<DbSet<Person>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, PersonDto>().ReverseMap();
            }).CreateMapper();
        }

        [Theory]
        [ClassData(typeof(GetPersonsByLocationQueryTestData))]
        public async Task GetPersonsByLocationQuery_SimpleDataGetList_ReturnsEqualsList(GetPersonsByLocationQuery contact)
        {

            var dataContacts = new List<Contact>
            {
                new Contact {  Id = 1,
                Email = "testupdat1e@gmail.com",
                PersonId = 3,
                Location = "Kırklareli",
                PhoneNumber = "032254788965",Person=new Person{Company="deneme2",Name="Aylin",Surname="Hayat" } },
                 new Contact {  Id = 2,
                Email = "testupdate2@gmail.com",
                PersonId = 2,
                Location = "Edirne",
                PhoneNumber = "022254788965" ,Person=new Person{Company="deneme3",Name="Güneş",Surname="Ateş"}},
                 new Contact {  Id = 3,
                Email = "testupdate3@gmail.com",
                PersonId = 2,
                Location = "Kırklareli",
                PhoneNumber = "012254788965",Person=new Person{Company="deneme",Name="Hasan",Surname="Ali"}
                },
            };

            var contactMock = dataContacts.AsQueryable().BuildMockDbSet();

            var dataPersons = new List<Person>
            {
               new Person{Id=1,Company="deneme2",Name="Aylin",Surname="Hayat"},
               new Person{Id=2,Company="deneme",Name="Hasan",Surname="Ali"},
               new Person{Id=3,Company="deneme3",Name="Güneş",Surname="Ateş"}
            };

            var personsMock = dataPersons.AsQueryable().BuildMockDbSet();


            _mockContext.Setup(m => m.Contacts).Returns(contactMock.Object);
            _mockContext.Setup(m => m.Persons).Returns(personsMock.Object);

            _getByLocationReportCommandHandler = new GetPersonsByLocationQueryHandler(_mockContext.Object, _mapper);


            var result = await _getByLocationReportCommandHandler.Handle(contact, new CancellationToken());
            Assert.IsType<List<PersonDto>>(result);
        }

    }
}
