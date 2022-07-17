using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace ElimeProject
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            /*  using IServiceScope scope = host.Services.CreateScope();
              IServiceProvider services = scope.ServiceProvider;
              try
              {
                  ApplicationContext dbContext = services.GetRequiredService<ApplicationContext>();
                  if (dbContext.Database.IsSqlServer())
                      dbContext.Database.Migrate();
              }
              catch (Exception ex)
              {
                  ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                  logger.LogError(ex, "An error occurred while migrating or seeding the database");
                  throw;
              }*/
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await AppSeeds.CreateDataAsync(services);
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
