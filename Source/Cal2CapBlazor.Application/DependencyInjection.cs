using System.Reflection;
using Cal2CapBlazor.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cal2CapBlazor.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection service)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        service.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        service.AddValidatorsFromAssembly(assembly);

        return service;
    }
}