using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Infrastructure;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
