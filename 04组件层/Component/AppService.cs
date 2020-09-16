using JMS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Component
{
    public class AppService
    {
        static IServiceProvider ServiceProvider {
            get {
                ServiceCollection services = new ServiceCollection();
                return services.BuildServiceProvider();
            }
        }

        //网关IP地址
        static string GatewayIP = AppConfigurtaion.AppSettings.GetSection("GatewayIP").Value;
        static string ConfigPort = AppConfigurtaion.AppSettings.GetSection("GatewayPort").Value;


        public static NetAddress[] GetNetAddress()
        {
            return new NetAddress[] {
                new NetAddress(GatewayIP, Convert.ToInt32(ConfigPort)) //网关地址
            };
        }
        public static JMSClient CreateMST()
        {
         
            var logger = ServiceProvider.GetService<ILogger<JMSClient>>();
            return new JMSClient(GatewayIP, Convert.ToInt32(ConfigPort), null, logger);
        }

        /// <summary>
        /// 通过服务名获取接口函数
        /// </summary>
        /// <param name="Namespace"></param>
        [Conditional("DEBUG")]
        public static void GetServiceClassCode(string Namespace)
        {
            Thread.Sleep(10000);//等服务启动完毕
            string path = Environment.CurrentDirectory+ "/APIService";
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            
            using (var client = CreateMST())
            {
                //找网关分配一个ConfigServiceAPI微服务实例
                var api = client.GetMicroService("ConfigServiceAPI");
                //调用服务的函数
                var ret = api.Invoke<string>("GetReferenceServiceByServiceName", Namespace);

                var values = ret.Split(",");
                foreach (string val in values)
                {
                    var retT = api.Invoke<string>("RegisterInfo", val);
                    var aA = JArray.Parse(retT);
                    foreach (var ss in aA)  //查找某个字段与值
                    {
                        var NamespaceAPI = client.GetMicroService(ss["Value"].ToString());
                        var code = NamespaceAPI.GetServiceClassCode(Namespace, ss["Value"].ToString());
                        File.WriteAllText(path+"\\" + ss["Value"]+".cs", code, Encoding.UTF8);
                    }

                   
                }
            }


        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="registerPort"></param>
        /// <param name="netAddress"></param>
        /// <param name="list"></param>
        public static void RegisterService(int registerPort, NetAddress[] netAddress, List<ServiceAddress> list)
        {
            //Type type = Assembly.GetExecutingAssembly("JMS").GetType("JMS.MicroServiceHost");

            ServiceCollection services = new ServiceCollection();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                loggingBuilder.AddConsole();
            });


            Assembly assembly = Assembly.Load("JMS.ServiceProvider");
            Type type = assembly.GetType("JMS.MicroServiceHost");
            object obj = Activator.CreateInstance(type, new object[] { services });
            foreach (ServiceAddress en in list)
            {
                var value = en.Libraries.Split(",");
                Type typeEntity = Assembly.Load(value[0]).GetType(value[1]);
                MethodInfo methodInfo = type.GetMethod("Register", new Type[] { typeof(string) });
                methodInfo = methodInfo.MakeGenericMethod(new Type[] { typeEntity });
                object[] invokeArgs = new object[] { en.ServiceName };
                methodInfo.Invoke(obj, invokeArgs);
            }
            MethodInfo BuildMethodInfo = type.GetMethod("Build");
            BuildMethodInfo.Invoke(obj, new object[] { registerPort, netAddress });
            MethodInfo RunMethodInfo = type.GetMethod("Run");
            RunMethodInfo.Invoke(obj, new object[] {});
        }


        public static void RS<T>(int port,string serviceName) where T : MicroServiceControllerBase
        {
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
            msp.Register<T>(serviceName);//服务名称为Hello world
            msp.Build(port, gateways).Run();
        }

        public class ServiceAddress
        {
            public string Libraries { get; set; }
            public string ServiceName { get; set; }
        }
    }
}
