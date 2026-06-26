using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Cal2CapBlazor.Application.Common;

namespace Cal2CapBlazor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection service)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        service.AddMediatR(configuration => {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    
        service.AddValidatorsFromAssembly(assembly);

        return service;
    }
}