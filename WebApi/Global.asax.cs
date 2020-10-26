using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApi.App_Start;

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //注册映射(文件配置方法)
            AutoMapperConfig.Config();

            // 创建注册组件的builder
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            //把当前程序集中的 Controller 都注册不要忘了.PropertiesAutowired()
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).PropertiesAutowired();

            //获取所有相关类库的程序集
            Assembly DAL = Assembly.Load("DAL");
            builder.RegisterAssemblyTypes(DAL).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            Assembly BLL = Assembly.Load("BLL");
            builder.RegisterAssemblyTypes(BLL).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            //获取所有相关类库的程序集
            //Assembly[] assemblies = new Assembly[] { Assembly.Load("DAL"), Assembly.Load("BLL") };

            //builder.RegisterAssemblyTypes(assemblies).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            var container = builder.Build();

            //注册系统级别的 DependencyResolver，这样当 api 框架创建 Controller 等对象的时候都是管 Autofac 要对象。

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
