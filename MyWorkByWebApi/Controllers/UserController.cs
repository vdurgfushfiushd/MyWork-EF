
using DTO;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWorkByWebApi.Controllers
{
    public class UserController : Controller
    {

        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/User/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/User/");

        [HttpGet]
        public ActionResult index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            using (var httpClient = new HttpClient())
            {
                List<UserDTO> list = new List<UserDTO>();
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "GetAll");
                //Uri address = new Uri(_baseAddress, "GetFilter?IsDeleted="+false);
                //返回的结果
                var response = await httpClient.GetAsync(address);
                //http相应是否请求成功
                if (response.IsSuccessStatusCode)
                {
                    //获取返回的结果（string类型）
                    var result = response.Content.ReadAsStringAsync().Result;
                    //将返回的结果转换为我们想要的类型
                    list = JsonConvert.DeserializeObject<List<UserDTO>>(result);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 用户新增界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(UserDTO userDTO)
        {
            using (var httpClient = new HttpClient())
            {
                Uri address = new Uri(_baseAddress, "Add");
                //将参数转换为字符串
                var userDTO_json = JsonConvert.SerializeObject(userDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(userDTO_json, Encoding.UTF8, "application/json");
                //请求的返回值
                var response = await httpClient.PostAsync(address, httpContent);
                //http请求是否成功
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail" });
                }
            }
        }

        /// <summary>
        /// 根据用户id删除指定的用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                Uri address = new Uri(_baseAddress, "Delete");
                var userUpdateDTOjson = JsonConvert.SerializeObject(Id);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/User/Index");
                }
                else
                {
                    return Redirect("/Error/Error");
                }
            }
        }

        /// <summary>
        /// 根据用户id获取要修改的用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetUser(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                Uri address = new Uri(_baseAddress, "Update");
                var userUpdateDTOjson = JsonConvert.SerializeObject(Id);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var userRoleDTO = new UserRoleDTO();
                var response = await httpClient.PostAsync(address, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    userRoleDTO = JsonConvert.DeserializeObject<UserRoleDTO>(result);
                }
                return View("Update",userRoleDTO);
            }
        }

        /// <summary>
        /// 修改用户信息和关联表的数据
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="Role_Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(UserUpdateDTO userUpdateDTO)
        {
            using (var httpClient = new HttpClient())
            {
                Uri address = new Uri(_baseAddress, "Update");
                var userUpdateDTOjson = JsonConvert.SerializeObject(userUpdateDTO);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(address, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    _ = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail" });
                }

            }
        }
        /// <summary>
        /// 根据用户id查询用户的所有关联角色名和权限名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowUserRoleModule(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                var userRoleModuleDTOs = new List<UserRoleModuleDTO>();
                Uri address = new Uri(_baseAddress, "ShowUserRoleModule?id=" + Id);
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    userRoleModuleDTOs = JsonConvert.DeserializeObject<List<UserRoleModuleDTO>>(result);
                }
                return View(userRoleModuleDTOs);
            }
        }
    }

   
}
