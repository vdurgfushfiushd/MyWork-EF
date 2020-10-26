using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.App_Start.Filter
{
    /// <summary>
    /// 全局过滤器
    /// </summary>
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //全局action控制过滤器注册
            filters.Add(new ActionFilter());

            //全局异常处理过滤器注册
            filters.Add(new ExceptionFilter());

            //全局权限访问控制过滤器注册
            //filters.Add(new AuthorizationFilter());
        }
    }
}