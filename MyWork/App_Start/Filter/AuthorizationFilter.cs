using IBLL;
using log4net;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.App_Start.Filter
{
    /// <summary>
    /// 权限的过滤器
    /// FilterAttribute
    /// </summary>
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public IUserBLL userBLL { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当前控制器名字
            string controllerName = filterContext.RouteData.Values["controller"] as string;
            //当前action名字
            string actionName = filterContext.RouteData.Values["action"] as string;
            //操作不是用户登录界面
            if (!(controllerName == "User" && actionName == "Login"))
            {
                var LoginName = filterContext.HttpContext.Session["UserName"];
                //先前没登录过
                if (LoginName == null)
                {
                    //filterContext.RequestContext.HttpContext.Response.Write("大兄弟，你还没有登录过");
                    filterContext.Result = new RedirectResult("/Other/UnLogin");
                }
                else
                {
                    //登录的用户的id
                    var UserId = filterContext.HttpContext.Session["UserId"].ToString();
                    //登录的用户的名字
                    var UserName = filterContext.HttpContext.Session["UserName"].ToString();
                    //控制器名字
                    var _controllerName = controllerName + "Controller";
                    //判断当前用户是否拥有此权限(特定用户有所有的权限)
                    var flag = userBLL.GetPermissionFlag(UserId, _controllerName, actionName);
                    if (!flag)
                        filterContext.RequestContext.HttpContext.Response.Write("大兄弟，你没有权限进行该操作");
                    //filterContext.Result = new RedirectResult("/General/PermissionsWarning");
                }
            }
        }     
    }
}