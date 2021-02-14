using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PhoneBook.API
{
   
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.ConfigureKestrel(serverOptions =>
               {

               })
              .UseStartup<Startup>()
              .UseSerilog(
                   (hostingContext, loggerConfiguration) =>
                       loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
               );
           });

    }
}
