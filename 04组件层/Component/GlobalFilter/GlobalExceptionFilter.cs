using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.GlobalFilter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            string errMsg = "GlobalExceptionFilter-OnException：" + ex.Message;
        }
    }
}
