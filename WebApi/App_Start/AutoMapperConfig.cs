using AutoMapper;
using WebApi.AutoMapperProfile;

namespace WebApi.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                //将要注册的配置文件注册
                cfg.AddProfile<UserMapperProfile>();
                cfg.AddProfile<RoleMapperProfile>();
                cfg.AddProfile<ModuleMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();

                //如果还有其他的实体配置文件，接着往下加就可以
            });
        }
    }
}