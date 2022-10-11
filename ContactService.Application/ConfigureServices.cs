using System.Reflection;
using AutoMapper;
using ContactService.Application.Common.Mappings;
using ContactService.Application.Contacts.Commands;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(CreatePersonCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(GetAllPersonsQuery).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(DeleteContactCommand).GetTypeInfo().Assembly);
        services.AddMediatR(typeof(UpdateContactCommand).GetTypeInfo().Assembly);


        #region Config AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<PersonMappingProfile>();
            cfg.AddProfile<ContactMappingProfile>();

        });
        var mapper = config.CreateMapper();
        #endregion
        return services;
    }
}
