using Microsoft.Extensions.Configuration;

namespace SMCM_Testing.UnitTests
{
    public class ConfigManager
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}
