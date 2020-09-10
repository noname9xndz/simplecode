using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Order.Infrastructure.Models.Base;

namespace Order.Api
{
    public class Program : OrderProgram
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}