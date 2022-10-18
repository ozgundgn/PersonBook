using AutoMapper;
using MediatR;
using MockQueryable.Moq;
using Moq;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Persistence;
using ReportService.Test.Models;
using Xunit;

namespace ReportService.Test.Queries
{
    public class GetAllReportsQueryTest
    {
        private Mock<ReportDbContext> _mockContext;

        private GetAllReportsQueryHandler _getAlleReportCommandHandler;
        private IMapper _mapper;

        public GetAllReportsQueryTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Report, ReportDto>().ReverseMap();
            }).CreateMapper();

            _mockContext = new Mock<ReportDbContext>();
            // Init
            _getAlleReportCommandHandler = new GetAllReportsQueryHandler(_mockContext.Object, _mapper);
        }

        [Theory]
        [ClassData(typeof(GetAllReportsQueryTestData))]
        public async Task GetAllReportsQuery_SimpleDataSend_ReturnsEquals(GetAllReportsQuery report)
        {
            //Arrange
            var guidId = Guid.NewGuid();
            var dataReports = new List<Report>
            {
              new Report {CreatedDate=DateTime.Now, Path="c//deneme",State=0,Uuid=guidId,Id=1 }
            };

            var reportsMock = dataReports.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Reports).Returns(reportsMock.Object);

            _getAlleReportCommandHandler = new GetAllReportsQueryHandler(_mockContext.Object, _mapper);

            // Act
            var result = await _getAlleReportCommandHandler.Handle(report, new CancellationToken());

            // Assert
            Assert.IsType<List<ReportDto>>(result);
        }
    }
}
