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
    /// 角色webapi控制器
    /// </summary>
    [RoutePrefix("api/Role/")]
    public class RoleController : ApiController
    {

        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        /// <summary>
        /// 获取所有的角色集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<RoleDTO> GetAll()
        {
            return roleBLL.GetFilter(e => e.IsDeleted == false);
        }

        /// <summary>
        /// 角色新增
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Add(JObject jObject)
        {
            var roleDTO = jObject.ToObject<RoleDTO>();
            await roleBLL.AddAsync(roleDTO);
            return new Response(true);
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Delete(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            await roleBLL.DeleteAsync(e => e.Id == Id);
            return new Response(true);
        }

        /// <summary>
        /// 获取要修改的角色及其模块
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public List<RoleModuleDTO> GetUpdateEntity(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            return roleBLL.GetUpdateEntity(Id);
        }

        /// <summary>
        /// 根据动态条件获取单个角色对象
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public RoleDTO GetEntity(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string,object>>();
            return roleBLL.GetEntity(dict);
        }

        /// <summary>
        /// 根据动态条件获取角色对象集合
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public List<RoleDTO> GetFilter(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string, object>>();
            return roleBLL.GetFilter(dict);
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Update(JObject jObject)
        {
            RoleDTO roleDTO = jObject.ToObject<RoleDTO>();
            var Module_Ids= jObject.ToObject<string[]>();
            await roleBLL.UpdateAsync(roleDTO, Module_Ids);
            return new Response(true);
        }

        /// <summary>
        /// 根据角色id查询关联角色的权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<RoleModuleDTO> ShowRoleModule(string Id)
        {
            return roleBLL.GetRoleModules(Id);
        }

    }
}
