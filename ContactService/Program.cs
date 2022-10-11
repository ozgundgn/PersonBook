using ContactService.Infrastructure.Persistence;
using EventBusRabbitMQ.Producer;
using EventRabbitMQ;
using RabbitMQ.Client;

namespace ContactService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();

            //Oluþturulan Serviceler
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();


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

            builder.Services.AddSingleton<EventBusRabbitMQProducer>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Initialise database
                using var scope = app.Services.CreateScope();
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationContactDbContextInitialiser>();
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