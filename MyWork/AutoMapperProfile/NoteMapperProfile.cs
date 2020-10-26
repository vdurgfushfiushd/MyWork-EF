using AutoMapper;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.AutoMapperProfile
{
    public class NoteMapperProfile : Profile
    {
        public NoteMapperProfile()
        {
            CreateMap<Note, NoteDTO>();
            CreateMap<NoteDTO, Note>();
            //下面还可以加上其他的映射文件
        }
    }
}