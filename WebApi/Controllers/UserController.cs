using Common;
using DTO;
using IBLL;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 用户webapi控制器
    /// </summary>
    [RoutePrefix("/api/User/")]
    public class UserController : ApiController
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        /// <summary>
        /// 全部查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UserDTO> GetAll()
        {
            return userBLL.GetFilter(e => e.IsDeleted == false);
        }

        /// <summary>
        /// 根据动态条件获取单个用户对象
        /// </summary>
        /// <param name="jObject">动态查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public UserDTO GetEntity(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string, object>>();
            return userBLL.GetEntity(dict);
        }

        /// <summary>
        /// 根据动态条件获取用户对象集合
        /// </summary>
        /// <param name="jObject">动态查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public List<UserDTO> GetFilter(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string, object>>();
            return userBLL.GetFilter(dict);
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Add(JObject jObject)
        {
            var userDTO = jObject.ToObject<UserDTO>();
            await userBLL.AddAsync(userDTO);
            return new Response(true); 
        }

        /// <summary>
        /// 根据用户id删除指定的用户信息
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Delete(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            await userBLL.DeleteAsync(e => e.Id == Id);
            return new Response(true);
        }

        /// <summary>
        /// 根据用户id获取要修改的用户
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public List<UserRoleDTO> GetUpdateEntity(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            return userBLL.GetUpdateEntity(Id);
        }

        /// <summary>
        /// 修改用户信息和关联表的数据
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Update(JObject jObject)
        {
            var userUpdateDTO = jObject.ToObject<UserUpdateDTO>();
            UserDTO userDTO = userUpdateDTO.UserDTO;
            string[] Role_Ids = userUpdateDTO.Role_Ids;
            await userBLL.UpdateAsync(userDTO, Role_Ids);
            return new Response(true);
        }

        /// <summary>
        /// 根据用户id查询用户的所有关联角色名和权限名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<UserRoleModuleDTO> ShowUserRoleModule(string Id)
        {
            return userBLL.GetUserRoleModule(Id);
        }
    }
}
