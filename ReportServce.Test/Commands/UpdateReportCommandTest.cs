using AutoMapper;
using MediatR;
using MockQueryable.Moq;
using Moq;
using ReportService.Application.Reports.Commands;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;
using ReportService.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportService.Test.Commands
{
    public class UpdateReportCommandTest
    {
        private Mock<ReportDbContext> _mockContext;

        private UpdateReportCommandHandler _updateReportCommandHandler;

        public UpdateReportCommandTest()
        {
            _mockContext = new Mock<ReportDbContext>();
            // Init
            _updateReportCommandHandler = new UpdateReportCommandHandler(_mockContext.Object);
        }

        [Theory]
        [ClassData(typeof(UpdateReportCommandTestData))]
        public async Task UpdateReportCommand_SimpleDataSend_ReturnsEquals(UpdateReportCommand report)
        {
            //Arrange
            var guidId = Guid.NewGuid();
            report.Uuid = guidId;
            var dataReports = new List<Report>
            {
              new Report {CreatedDate=DateTime.Now, Path="c//deneme",State=0,Uuid=guidId }
            };

            var reportsMock = dataReports.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Reports).Returns(reportsMock.Object);

            _updateReportCommandHandler = new UpdateReportCommandHandler(_mockContext.Object);

            // Act
            var result = await _updateReportCommandHandler.Handle(report, new CancellationToken());

            // Assert
            Assert.IsType<Unit>(result);
        }
        [Fact]
        public async Task UpdateReportCommand_NullDataSend_ReturnsEqualsArgumentException()
        {

            await Assert.ThrowsAsync<ArgumentException>(async () => await _updateReportCommandHandler.Handle(null, new CancellationToken()));
        }
    }
}
