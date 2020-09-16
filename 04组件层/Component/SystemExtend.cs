using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Component
{
    /// <summary>
    /// 系统扩展方法
    /// </summary>
    public static class SystemExtend
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
