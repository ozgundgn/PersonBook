using AutoMapper;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ReportService.Domain.Entities;

namespace ReportService.Application.Common.Mappings
{
    public class ReportMappingProfile:Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<Report, CreateReportCommand>().ReverseMap();
            CreateMap<Report, UpdateReportCommand>().ReverseMap();
            CreateMap<Report, ReportDto>().ReverseMap();


        }
    }
}
