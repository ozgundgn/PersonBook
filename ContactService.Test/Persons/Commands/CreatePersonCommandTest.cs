using AutoMapper;
using ContactService.Application.Common.Interfaces;
using ContactService.Application.Contacts.Commands;
using ContactService.Application.Persons.Commands;
using ContactService.Domain.Entities;
using ContactService.Infrastructure.Persistence;
using ContactService.Test.Models;
using FakeItEasy.Sdk;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;
namespace ContactService.Test.Commands
{
    public class CreatePersonCommandTest
    {
        //private readonly Mock<IRequestHandler<CreateContactCommand,int>> _createContactCommandHandlerMock;
        private  Mock<ContactDbContext> _mockContext;
        private IMapper _mapper;

        private CreatePersonCommandHandler _createPersonCommandHandler;

        public CreatePersonCommandTest()
        {
            _mapper = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<Person, CreatePersonCommand>().ReverseMap();
           }).CreateMapper();
            _mockContext = new Mock<ContactDbContext>();
            // Init
            _createPersonCommandHandler = new CreatePersonCommandHandler(_mockContext.Object, _mapper);

        }

        [Theory]
        [ClassData(typeof(CreatePersonCommandTestData))]
        public async Task CreatePersonCommand_SimpleDataSend_ReturnsEquals(CreatePersonCommand contact)
        {
            //Arrange
            var dataPersons = new List<Person>
            {
               new Person{Id=1,Company="deneme2",Name="Aylin",Surname="Hayat"},
               new Person{Id=2,Company="deneme",Name="Hasan",Surname="Ali"},
               new Person{Id=3,Company="deneme3",Name="Güneþ",Surname="Ateþ"}
            };

            var personsMock = dataPersons.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Persons).Returns(personsMock.Object);

            _createPersonCommandHandler = new CreatePersonCommandHandler(_mockContext.Object, _mapper);

            // Act
            var result = await _createPersonCommandHandler.Handle(contact, new CancellationToken());
            
            // Assert
            Assert.IsType<Guid>(result);
        }
        [Fact]
        public async Task CreatePersonCommand_NullDataSend_ReturnsEqualsArgumentException()
        {
         
            await Assert.ThrowsAsync<ArgumentException>(async () => await _createPersonCommandHandler.Handle(null, new CancellationToken()));
        }

    }
}