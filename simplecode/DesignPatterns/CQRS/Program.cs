using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CQRSTest
{
    public class Program
    {
        //https://github.com/gregoryyoung/m-r/tree/master/SimpleCQRS
        //https://github.com/asc-lab/dotnet-cqrs-intro
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