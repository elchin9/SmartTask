using Autofac;
using AutoMapper;
using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.TaskAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Identity;
using SmartSolution.Identity.Auth;
using SmartSolution.Infrastructure;
using SmartSolution.Infrastructure.Idempotency;
using SmartSolution.Infrastructure.Identity;
using SmartSolution.Infrastructure.Repositories;
using SmartSolution.Organization;
using SmartSolution.Task;
using SmartSolution.User;
using System.Collections.Generic;

namespace SmartSolutionCRM.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            AutofacHelper.RegisterCqrsTypes<ApplicationModule>(builder);
            AutofacHelper.RegisterCqrsTypes<IdentityModule>(builder);
            AutofacHelper.RegisterCqrsTypes<UserModule>(builder);
            AutofacHelper.RegisterCqrsTypes<OrganizationModule>(builder);
            AutofacHelper.RegisterCqrsTypes<TaskModule>(builder);

            // Services

            // Repositories
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CrmTaskRepository>()
                   .As<ICrmTaskRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<OrganizationRepository>()
                   .As<IOrganizationRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<RoleRepository>()
              .As<IRoleRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<ClaimsManager>()
                .As<IClaimsManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserManager>()
                .As<IUserManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();


            // AutoMapper
            AutofacHelper.RegisterAutoMapperProfiles<IdentityModule>(builder);
            AutofacHelper.RegisterAutoMapperProfiles<ApplicationModule>(builder);
            AutofacHelper.RegisterAutoMapperProfiles<UserModule>(builder);
            AutofacHelper.RegisterAutoMapperProfiles<OrganizationModule>(builder);
            AutofacHelper.RegisterAutoMapperProfiles<TaskModule>(builder);

            builder.Register(ctx =>
            {
                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in ctx.Resolve<IList<Profile>>()) cfg.AddProfile(profile);
                });
                return mapperConfiguration.CreateMapper();
            })
                .As<IMapper>()
                .SingleInstance();
        }
    }
}
