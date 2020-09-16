using JMS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace TestApplication
{
    class Program
    {
        static IServiceProvider ServiceProvider;
        static JMSClient CreateMST()
        {
            var logger = ServiceProvider.GetService<ILogger<JMSClient>>();
            return new JMSClient("172.30.77.87", 7900, null, logger);
        }
        static void Main(string[] args)
        {
            Thread.Sleep(10000);//等服务启动完毕

            ServiceCollection services = new ServiceCollection();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                loggingBuilder.AddConsole();
            });
            ServiceProvider = services.BuildServiceProvider();

            using (var client = CreateMST())
            {
                var api = client.GetMicroService("ConfigServiceAPI");
                var code = api.GetServiceClassCode("TestApplication", "ConfigServiceAPI");
                File.WriteAllText("../../../ConfigServiceAPI.cs", code, Encoding.UTF8);
            }

            //using (var client = CreateMST())
            //{
            //    var api = client.GetMicroService<HelloWorldApi>();
            //    var ret = api.Hello(DateTime.Now);
            //    Console.WriteLine(ret);
            //}

        }
    }
}
