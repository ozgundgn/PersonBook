using ContactService.Application.Common.Interfaces;
using ContactService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddEntityFrameworkNpgsql().AddDbContext<ContactDbContext>(opt =>
         {
             var connectionString = configuration.GetConnectionString("ContactDbConnection");
             opt.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(typeof(ContactDbContext).Assembly.FullName));


         });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ContactDbContext>());

        services.AddScoped<ApplicationContactDbContextInitialiser>();
        return services;
    }
}

