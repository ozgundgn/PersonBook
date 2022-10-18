using AutoMapper;
using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using ContactService.Application.Persons.Queries;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Contacts.Queries;
using ContactService.Test.Models;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Data.Entity.Infrastructure;
using Xunit;
namespace ContactService.Test.Persons.Queries
{
    public class GetAllPersonsQueryTest
    {
        private  Mock<ContactDbContext> _mockContext;
        private  Mock<DbSet<Person>> _mockSetPerson;
        private IMapper _mapper;



        private GetAllPersonsQueryHandler _getAllPersonsCommandHandler;

        public GetAllPersonsQueryTest()
        {
            _mockContext = new Mock<ContactDbContext>();
            _mockSetPerson = new Mock<DbSet<Person>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, PersonDto>().ReverseMap();
            }).CreateMapper();
        }

        [Theory]
        [ClassData(typeof(GetAllPersonsQueryTestData))]
        public async Task GetAllPersonsCommand_SimpleDataSend_ReturnsEquals(GetAllPersonsQuery contact)
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

          
            var personsMock= dataPersons.AsQueryable().BuildMockDbSet();
            _mockContext.Setup(m => m.Persons).Returns(personsMock.Object);
          

            _getAllPersonsCommandHandler = new GetAllPersonsQueryHandler(_mockContext.Object, _mapper);


            var result = await _getAllPersonsCommandHandler.Handle(contact, new CancellationToken());
            Assert.IsType<List<PersonDto>>(result);
        }

    }
}