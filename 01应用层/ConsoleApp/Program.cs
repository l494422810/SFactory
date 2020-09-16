using JMS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 7902; //提供微服务的端口
            var gateways = new NetAddress[] {
                new NetAddress("172.30.77.87" , 7900) //网关地址
            };
            ServiceCollection services = new ServiceCollection();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                loggingBuilder.AddConsole();
            });
            var msp = new MicroServiceHost(services);
            msp.Register<HelloworldController>("Hello world");//服务名称为Hello world
            msp.Build(port, gateways).Run();


          
        }

    }
}
