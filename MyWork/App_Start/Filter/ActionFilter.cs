using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.App_Start.Filter
{
    /// <summary>
    /// action的过滤器
    /// FilterAttribute
    /// </summary>
    public class ActionFilter : ActionFilterAttribute, IActionFilter
    {
        private static ILog log = LogManager.GetLogger(typeof(ActionFilter));
        
        //action执行后
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            if (!(controllerName == "User" && actionName == "Login"))
            {
                var LoginName = filterContext.HttpContext.Session["UserName"];
                //先前没登录过
                if (LoginName == null)
                {
                    log.DebugFormat("游客执行了" + controllerName + "控制器的" + actionName + "方法");
                }
                else
                {
                    var userName = LoginName.ToString();
                    log.DebugFormat(userName + "执行了" + controllerName + "控制器的" + actionName + "方法");
                }
            }
        }

        //action执行时
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            if (!(controllerName == "User" && actionName == "Login"))
            {
                var LoginName = filterContext.HttpContext.Session["UserName"];
                //先前没登录过
                if (LoginName == null)
                {
                    log.DebugFormat("游客正在执行" + controllerName + "控制器的" + actionName + "方法");
                }
                else
                {
                    var userName = LoginName.ToString();
                    log.DebugFormat(userName + "正在执行" + controllerName + "控制器的" + actionName + "方法");
                }
            }
        }
    }
}