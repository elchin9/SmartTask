using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSolution.Infrastructure;
using SmartSolution.Infrastructure.Database;
using SmartSolutionCRM.Extensions;
using SmartSolutionCRM.Infrastructure.DBSeed;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSolutionCRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.MigrateDbContext<SmartSolutionDbContext>(async (context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var settings = services.GetService<IOptions<SmartSolutionSettings>>();
                var logger = services.GetService<ILogger<SmartSolutionDbContextSeed>>();

                var seeder = new SmartSolutionDbContextSeed();
                await seeder.SeedAsync(context, env, settings, logger);
            });

            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

    }
}
