using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL
{
    public class Ut
    {
        public static string GetUrlEU()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();
            return Configuration.GetSection("HostAdress").GetSection("HondaEU").Value;
        }

        public static string GetUrlGM()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();
            return Configuration.GetSection("HostAdress").GetSection("HondaGM").Value;
        }


        public static string GetUrlJP()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();
            return Configuration.GetSection("HostAdress").GetSection("HondaJP").Value;
        }

        public static string [] GetUrls()
        {
            string[] urls = new string[3];

            urls[0] = GetUrlEU();
            urls[1] = GetUrlGM();
            urls[2] = GetUrlJP();

            return urls;
        }

        public static string GetPrefixEU()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();
            return Configuration.GetSection("prefix").GetSection("EU").Value;
        }

        public static string GetPrefixGM()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();
            return Configuration.GetSection("prefix").GetSection("GM").Value;
        }
        public static string GetPrefixJP()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();
            return Configuration.GetSection("prefix").GetSection("JP").Value;
        }
    }
}
