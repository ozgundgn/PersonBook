using EventRabbitMQ;
using MediatR;
using RabbitMQ.Client;
using ReportService.BackgroundWorker;
using ReportService.Consumers;
using ReportService.Infrastructure.Persistence;
using System.Reflection;

namespace ReportService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHostedService<ApiWorker>();
            builder.Services.AddReportInfrastructureServices(builder.Configuration);
            builder.Services.AddReportApplicationServices();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


            //RabbitMq için

            builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = builder.Configuration["EventBus:HostName"]
                }; //IConnectionfactory tipinde

                if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:UserName"]))
                {
                    factory.UserName = builder.Configuration["EventBus:UserName"];
                }
                if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:Password"]))
                {
                    factory.Password = builder.Configuration["EventBus:Password"];
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger);
            });



            builder.Services.AddSingleton<EventBusReportCreateConsumer>();

            //Excel için
            builder.Services.UseIcgNetCoreUtilitiesSpreadsheet();


            var app = builder.Build();
            //
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();

                // Initialise database
                using var scope = app.Services.CreateScope();
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationReportDbContextInitialiser>();
                initialiser.InitialiseAsync();
                initialiser.SeedAsync();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}