using System.Reflection;
using AutoMapper;
using FluentValidation;
using HashidsNet;
using HeyTaxi.VehicleService.Application.Configuration.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HeyTaxi.VehicleService.Application;

public static class Extension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IHashids>(_ => new Hashids("HeyTaxi", 10));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton(p =>
            new MapperConfiguration(c => c.AddProfile(new MapperProfile(p.GetRequiredService<IHashids>())))
            .CreateMapper());

        return services;
    }
}