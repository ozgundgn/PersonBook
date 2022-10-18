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
    public class GetReportsByLocationQueryTest
    {
        private Mock<ReportDbContext> _mockContext;

        private GetReportsByLocationQueryHandler _getAlleReportCommandHandler;
        private IMapper _mapper;

        public GetReportsByLocationQueryTest()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Report, ReportDto>().ReverseMap();
            }).CreateMapper();

            _mockContext = new Mock<ReportDbContext>();
            // Init
            _getAlleReportCommandHandler = new GetReportsByLocationQueryHandler(_mockContext.Object, _mapper);
        }

        [Theory]
        [ClassData(typeof(GetReportsByLocationQueryTestData))]
        public async Task GetAllReportsQuery_SimpleDataGet_ReturnsEqualsReportList(GetReportsByLocationQuery report)
        {
            //Arrange
            var guidId = Guid.NewGuid();
            var dataReports = new List<Report>
            {
              new Report {CreatedDate=DateTime.Now, Path="c//deneme",State=0,Uuid=guidId,Id=1 }
            };

            var reportsMock = dataReports.AsQueryable().BuildMockDbSet();

            _mockContext.Setup(m => m.Reports).Returns(reportsMock.Object);

            _getAlleReportCommandHandler = new GetReportsByLocationQueryHandler(_mockContext.Object, _mapper);

            // Act
            var result = await _getAlleReportCommandHandler.Handle(report, new CancellationToken());

            // Assert
            Assert.IsType<List<ReportDto>>(result);
        }
    }
}
