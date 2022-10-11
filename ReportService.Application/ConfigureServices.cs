using System.Reflection;
using AutoMapper;
using MediatR;
using ReportService.Application.Common.Mappings;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddReportApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(CreateReportCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(UpdateReportCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(DeleteReportCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetReportsByLocationQuery).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetAllReportsQuery).GetTypeInfo().Assembly);

        #region Config AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<ReportMappingProfile>();

        });
        var mapper = config.CreateMapper();
        #endregion
        return services;
    }
}
