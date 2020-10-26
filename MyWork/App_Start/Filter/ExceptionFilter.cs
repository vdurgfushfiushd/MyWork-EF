using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.App_Start.Filter
{
    /// <summary>
    /// 错误过滤器
    /// </summary>
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(typeof(ExceptionFilter));
        public void OnException(ExceptionContext filterContext)
        {
            //错误的操作
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;

            //将错误记录到日志当中
            //log.Error("出现未处理异常", filterContext.Exception);
            //filterContext.Result = new RedirectResult("/General/Error");
            filterContext.RequestContext.HttpContext.Response.Write(string.Format("{0}:{1}发生异常!{2}",controller, action, filterContext.Exception.Message));
            filterContext.ExceptionHandled = true;
        }
    }
}