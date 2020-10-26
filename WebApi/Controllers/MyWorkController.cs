using DTO;
using IBLL;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// webapi控制器
    /// </summary>
    [RoutePrefix("api/MyWork/")]
    public class MyWorkController : ApiController
    {
        public IUserBLL userBLL { get; set; }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public UserDTO Login(JObject jObject)
        {
            UserDTO userDTO = jObject.ToObject<UserDTO>();
            return userBLL.GetEntity(e => e.Name == userDTO.Name && e.Password == userDTO.Password && e.IsDeleted == false);
        }
    }
}
