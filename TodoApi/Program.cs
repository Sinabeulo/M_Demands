using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApi.Models;
using TodoApi.Data;
using System.Threading.Tasks;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 2.1 Original
            CreateWebHostBuilder(args).Build().Run();

            // Use IHost
            //var host = new HostBuilder().Build();


            //SeedDatabase(host);
            //host.Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder()

        //private static void SeedDatabase(IHost host)
        //{
        //    var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();

        //    using (var scope = scopeFactory.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetRequiredService<TodoContext>();

        //        if (context.Database.EnsureCreated())
        //        {
        //            try
        //            {
        //                SeedData.Initialize(context);
        //            }
        //            catch (Exception ex)
        //            {
        //                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        //                logger.LogError(ex, "A database seeding error occurred.");
        //            }
        //        }
        //    }
        //}
    }
}
