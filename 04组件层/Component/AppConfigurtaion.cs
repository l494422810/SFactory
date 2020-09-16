using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace Component
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppConfigurtaion
    {
        public static IConfiguration AppSettings { get; set; }

        static AppConfigurtaion()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            AppSettings = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource{ Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

        public static IConfiguration BuilderConfiguration(string path)
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            return new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = path, ReloadOnChange = true })
            .Build();
        }

    }
}
