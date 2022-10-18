using AutoMapper;
using MediatR;
using MockQueryable.Moq;
using Moq;
using ReportService.Application.Reports.Commands;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;
using ReportService.Test.Models;
using Xunit;


namespace ReportService.Test.Commands
{
    public class DeleteReportCommandTest
    {
        private Mock<ReportDbContext> _mockContext;

        private DeleteReportCommandHandler _updateReportCommandHandler;

        public DeleteReportCommandTest()
        {
            _mockContext = new Mock<ReportDbContext>();
            // Init
            _updateReportCommandHandler = new DeleteReportCommandHandler(_mockContext.Object);
        }

        [Theory]
        [ClassData(typeof(DeleteReportCommandTestData))]
        public async Task DeleteReportCommand_SimpleDataSend_ReturnsEquals(DeleteReportCommand report)
        {
            //Arrange
            var guidId = Guid.NewGuid();
            var dataReports = new List<Report>
            {
              new Report {CreatedDate=DateTime.Now, Path="c//deneme",State=0,Uuid=guidId,Id=1 }
            };

            var reportsMock = dataReports.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Reports).Returns(reportsMock.Object);

            _updateReportCommandHandler = new DeleteReportCommandHandler(_mockContext.Object);

            // Act
            var result = await _updateReportCommandHandler.Handle(report, new CancellationToken());

            // Assert
            Assert.IsType<Unit>(result);
        }
        [Fact]
        public async Task DeleteReportCommand_NullDataSend_ReturnsEqualsArgumentException()
        {

            await Assert.ThrowsAsync<ArgumentException>(async () => await _updateReportCommandHandler.Handle(null, new CancellationToken()));
        }
    }
}
