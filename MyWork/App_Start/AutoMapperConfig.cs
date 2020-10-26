using AutoMapper;
using MyWork.AutoMapperProfile;

namespace MyWork.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                //将要注册的配置文件注册
                cfg.AddProfile<UserMapperProfile>();
                cfg.AddProfile<RoleMapperProfile>();
                cfg.AddProfile<ModuleMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();
            });
        }
    }
}