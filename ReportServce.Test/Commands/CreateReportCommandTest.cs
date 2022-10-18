using AutoMapper;
using MockQueryable.Moq;
using Moq;
using ReportService.Application.Reports.Commands;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;
using ReportService.Test.Models;
using Xunit;

namespace ReportService.Test.Commands
{
    public class CreateReportCommandTest
    {
        private Mock<ReportDbContext> _mockContext;
        private IMapper _mapper;

        private CreateReportCommandHandler _createPersonCommandHandler;

        public CreateReportCommandTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Report, CreateReportCommand>().ReverseMap();
            }).CreateMapper();
            _mockContext = new Mock<ReportDbContext>();
            // Init
            _createPersonCommandHandler = new CreateReportCommandHandler(_mockContext.Object, _mapper);

        }

        [Theory]
        [ClassData(typeof(CreateReportCommandTestData))]
        public async Task CreateReportCommand_SimpleDataSend_ReturnsEquals(CreateReportCommand contact)
        {
            //Arrange
            var dataReports = new List<Report>
            {
              new Report {CreatedDate=DateTime.Now, Path="c//deneme",State=0,Uuid=Guid.NewGuid() },
              new Report {CreatedDate=DateTime.Now, Path="c//deneme",State=0,Uuid=Guid.NewGuid() }
            };

            var reportsMock = dataReports.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Reports).Returns(reportsMock.Object);

            _createPersonCommandHandler = new CreateReportCommandHandler(_mockContext.Object, _mapper);

            // Act
            var result = await _createPersonCommandHandler.Handle(contact, new CancellationToken());

            // Assert
            Assert.IsType<Guid>(result);
        }
        [Fact]
        public async Task CreateReportCommand_NullDataSend_ReturnsEqualsArgumentException()
        {

            await Assert.ThrowsAsync<ArgumentException>(async () => await _createPersonCommandHandler.Handle(null, new CancellationToken()));
        }
    }
}
