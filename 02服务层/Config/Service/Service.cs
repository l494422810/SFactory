using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Component;
namespace Config.Service
{
    public class Service: MicroServiceControllerBase
    {
        /// <summary>
        /// 注册信息
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>JSON结果值</returns>
        public string RegisterInfo(string value)
        {
            var section =  AppConfigurtaion.BuilderConfiguration("serviceregister.json").GetSection(value+ ":Register");
            return section.GetChildren().ToList().ToJson();
        }

        /// <summary>
        /// 通过服务名获依赖的服务
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns></returns>
        public string GetReferenceServiceByServiceName(string Namespace)
        {
            var value = AppConfigurtaion.BuilderConfiguration("serviceregister.json").GetSection(Namespace + ":AIPCodeServiceName").Value;
            return value;
        }
    }
}
