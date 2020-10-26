using System.Web.Http;
using WebActivatorEx;
using WebApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Swagger配置
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WebApi");
                        //设置接口描述xml路径地址
                        c.IncludeXmlComments(string.Format("{0}/bin/WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory));
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("My webapi"); 
                    });
        }
    }
}
