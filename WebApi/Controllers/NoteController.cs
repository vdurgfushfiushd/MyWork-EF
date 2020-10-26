using Common;
using DTO;
using IBLL;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 日记webapi控制器
    /// </summary>
    [RoutePrefix("api/Note/")]
    public class NoteController : ApiController
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        /// <summary>
        /// 获取所有的日记集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<NoteDTO> GetAll()
        {
            return noteBLL.GetFilter(e => e.IsDeleted == false);
        }

        /// <summary>
        /// 日记新增
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Add(JObject jObject)
        {
            NoteDTO noteDTO = jObject.ToObject<NoteDTO>();
            noteDTO.UserId = noteDTO.UserId;
            await noteBLL.AddAsync(noteDTO);
            return new Response(true);
        }

        /// <summary>
        /// 日记删除
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Delete(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            await noteBLL.DeleteAsync(e => e.Id == Id && e.IsDeleted == false);
            return new Response(true);
        }

        /// <summary>
        /// 根据日记id获取要修改的日记对象
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public NoteDTO GetUpdateEntity(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            return noteBLL.GetEntity(e => e.Id == Id && e.IsDeleted == false);
        }

        /// <summary>
        /// 根据动态条件获取单个日记对象
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpGet]
        public NoteDTO GetEntity(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string, object>>();
            return noteBLL.GetEntity(dict);
        }

        /// <summary>
        /// 根据动态条件获取日记对象集合
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpGet]
        public List<NoteDTO> GetFilter(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string,object>>();
            return noteBLL.GetFilter(dict);
        }

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Update(JObject jObject)
        {
            NoteDTO noteDTO = jObject.ToObject<NoteDTO>();
            await noteBLL.UpdateAsync(noteDTO);
            return new Response(true);
        }

        /// <summary>
        /// 获取我的日志
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        public List<NoteDTO> MyNote(string UserId)
        {
            return noteBLL.GetFilter(e => e.UserId == UserId&&e.IsDeleted==false);
        }
    }
}
