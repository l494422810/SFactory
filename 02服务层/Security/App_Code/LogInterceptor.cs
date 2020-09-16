using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LogInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        try
        {
            invocation.Proceed();
            string value = invocation.Method.Name;
        }
        catch (Exception ex)
        {
            string value = invocation.Method.Name + " " + ex.ToString();
        }
    }
}