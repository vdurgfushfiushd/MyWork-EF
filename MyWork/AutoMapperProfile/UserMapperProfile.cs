using AutoMapper;
using DTO;
using Model;

namespace MyWork.AutoMapperProfile
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User,UserDTO>();
            CreateMap<UserDTO, User>();
            //下面还可以加上其他的映射文件
        }
    }
}