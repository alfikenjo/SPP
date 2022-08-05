using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Frontend_SPP.Common
{
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
