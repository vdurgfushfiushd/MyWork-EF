using Autofac;
using Autofac.Integration.Mvc;
using MyWork.App_Start;
using MyWork.App_Start.Filter;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyWork
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //启动日志系统
            log4net.Config.XmlConfigurator.Configure();

            //注册映射(文件配置方法)
            AutoMapperConfig.Config();

            //注册全局过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //autofac的配置
            var builder = new ContainerBuilder();
            
            //把当前程序集中的 Controller 都注册,不要忘了.PropertiesAutowired()            
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //获取DAL类库的程序集
            Assembly DAL = Assembly.Load("DAL");
            builder.RegisterAssemblyTypes(DAL).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            //注册全局过滤器
            builder.RegisterFilterProvider();

            //获取BLL类库的程序集
            Assembly BLL = Assembly.Load("BLL");
            builder.RegisterAssemblyTypes(BLL).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            //builder.RegisterType<ChatHub>();

            //注册过滤器,以便于在过滤器中使用依赖注入
            var container = builder.Build();

            //注册系统级别的 DependencyResolver，这样当 MVC 框架创建 Controller 等对象的时候都是管 Autofac 要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
