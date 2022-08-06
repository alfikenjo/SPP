using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
               webBuilder.UseIISIntegration();
               webBuilder.UseStartup<Startup>();
           });

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = long.MaxValue;
                })
                .UseIISIntegration()
                .Build();
        }
    }
}
