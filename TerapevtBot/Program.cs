using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TerapevtBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    var migrationsAssembly = typeof(Data.ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
                    services.AddDbContext<ApplicationDbContext>(options =>
                                                      options.UseNpgsql("Host=92.255.196.53;Port=5432;Database=marketing;Username=denis;Password=Ip83NqgzgUl6*XKU;", b =>
                                                                  b.MigrationsAssembly(migrationsAssembly)));
                });
    }
}
