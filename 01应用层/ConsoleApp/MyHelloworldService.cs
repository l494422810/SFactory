using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class HelloworldController : MicroServiceControllerBase
    {
        /// <summary>
        /// 哈喽方法
        /// </summary>
        /// <param name="time">我当前的时间</param>
        /// <returns>中文问候语</returns>
        public string Hello(DateTime time)
        {
            return $"你好，你给的时间是： {time.ToShortDateString()}";
        }

        /// <summary>
        /// 测试中
        /// </summary>
        /// <returns>测试中</returns>
        public string Test()
        {
            return "ddd";
        }
    }
}
