using ApiQF.Config;
using ApiQF.Services;
using Microsoft.Extensions.Configuration;

namespace ApiQF.Tests
{
    internal class TestHelper
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public static SatelliteConfig GetSatelliteConfig()
        {
            var config = GetConfiguration();
            var satelliteConfig = new SatelliteConfig();
            config.Bind("SatellitesData", satelliteConfig);
            return satelliteConfig;
        }

        public static MessageService GetMessageService()
        {
            return new MessageService();
        }
    }
}