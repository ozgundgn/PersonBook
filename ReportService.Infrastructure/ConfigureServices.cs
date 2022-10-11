
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReportService.Application.Common.Interfaces;
using ReportService.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddReportInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddEntityFrameworkNpgsql().AddDbContext<ReportDbContext>(opt =>
         {
             var connectionString = configuration.GetConnectionString("ReportDbConnection");
             opt.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(typeof(ReportDbContext).Assembly.FullName));

         });
        services.AddScoped<IApplicationReportDbContext>(provider => provider.GetRequiredService<ReportDbContext>());
        services.AddScoped<ApplicationReportDbContextInitialiser>();




        return services;
    }
}

