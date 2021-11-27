using Autofac;
using AutoMapper;
using FluentValidation;
using MediatR;
using SmartSolution.SharedKernel.Infrastructure.Queries;
using System.Reflection;

namespace SmartSolution.Infrastructure
{
    public class AutofacHelper
    {
        private static Assembly GetAssembly<T>()
        {
            return typeof(T).GetTypeInfo().Assembly;
        }

        public static void RegisterCqrsTypes<T>(ContainerBuilder builder)
        {
            var assemby = GetAssembly<T>();

            //Register all the Command classes(they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(assemby)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement IAsyncNotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(assemby)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(assemby)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            // Queries
            builder.RegisterAssemblyTypes(assemby)
                .Where(t => t.IsAssignableTo<IQuery>() && !t.IsAbstract && t.IsPublic && t.IsClass)
                .As(t => t.GetInterfaces()[0])
                .InstancePerLifetimeScope();
        }

        public static void RegisterAutoMapperProfiles<T>(ContainerBuilder builder)
        {
            var assemby = GetAssembly<T>();

            // AutoMapper Profiles
            builder.RegisterAssemblyTypes(assemby)
                .Where(t => t.BaseType == typeof(Profile) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();
        }
    }
}
