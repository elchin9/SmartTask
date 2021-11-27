using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using SmartSolution.Domain.AggregatesModel.OrganizationAggregate;
using SmartSolution.Domain.AggregatesModel.RoleAggregate;
using SmartSolution.Domain.AggregatesModel.UserAggregate;
using SmartSolution.Infrastructure;
using SmartSolution.Infrastructure.Database;
using SmartSolution.SharedKernel.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Infrastructure.DBSeed
{
    public class SmartSolutionDbContextSeed
    {
        public async Task SeedAsync(
           SmartSolutionDbContext context,
           IWebHostEnvironment env,
           IOptions<SmartSolutionSettings> settings,
           ILogger<SmartSolutionDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(SmartSolutionDbContextSeed));

            using (context)
            {
                await policy.ExecuteAsync(async () =>
                {
                    await context.SaveChangesAsync();

                    var systemUser = await context.Users.SingleOrDefaultAsync(u => u.UserName == "smart_solution_admin");

                    if (systemUser == null)
                    {
                        var superAdminRole = new Role(RoleParametr.SuperAdmin.Name);
                        await context.Roles.AddAsync(superAdminRole);

                        var adminRole = new Role(RoleParametr.Admin.Name);
                        await context.Roles.AddAsync(adminRole);

                        var userRole = new Role(RoleParametr.User.Name);
                        await context.Roles.AddAsync(userRole);

                        var organization = new Organization();
                        organization.AddToInfo("SmartSolution", "994514677569", "Baku");
                        await context.Organization.AddAsync(organization);

                        await context.SaveChangesAsync();

                        var user = new User
                        ("smart_solution_admin", "system.user@smart.az", PasswordHasher.HashPassword("smart_solution_admin", "Sm@rt2@2!"), "The Boss", "Smart Solution CRM", organization.Id);

                        user.AddToRole(superAdminRole.Id);
                        await context.Users.AddAsync(user);
                        await context.SaveChangesAsync();
                    }
                });
            }
        }


        private AsyncRetryPolicy CreatePolicy(ILogger<SmartSolutionDbContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().WaitAndRetryAsync(
                retries,
                retry => TimeSpan.FromSeconds(5),
                (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogTrace(
                        $"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                }
            );
        }
    }
}
