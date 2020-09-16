using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.GlobalFilter
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //LogHelper.Info("OnActionExecuting");
            //执行方法前先执行这
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //LogHelper.Info("OnActionExecuted");
            //执行方法后执行这
        }
    }
}
