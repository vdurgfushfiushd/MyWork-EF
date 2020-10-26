using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWorkByWebApi.Controllers
{
    public class RoleController : Controller
    {
        
        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/Role/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/Role/");

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            using (var httpClient = new HttpClient())
            {
                List<RoleDTO> list = new List<RoleDTO>();
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "GetAll");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<RoleDTO>>(result);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 角色新增界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(RoleDTO roleDTO)
        {
            using (var httpClient = new HttpClient())
            {
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "Add");
                //将参数转换为字符串
                var roleDTO_json = JsonConvert.SerializeObject(roleDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(roleDTO_json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(address, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail"});
                }
            }
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<RoleDTO>();
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "Delete");
                var userUpdateDTOjson = JsonConvert.SerializeObject(Id);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Role/Index");
                }
                else
                {
                    return Redirect("/Error/Error");
                }
            }
        }

        /// <summary>
        /// 角色修改界面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetRole(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                var rolemoduleDTO = new RoleModuleDTO();
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "GetUpdateEntity");
                var userUpdateDTOjson = JsonConvert.SerializeObject(Id);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    rolemoduleDTO = JsonConvert.DeserializeObject<RoleModuleDTO>(result);
                }
                return View(rolemoduleDTO);
            }
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="Module_Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(RoleModuleDTO roleModuleDTO)
        {
            using (var httpClient = new HttpClient())
            {
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "Update");
                //将参数转换为字符串
                var roleUpdateDTOjson = JsonConvert.SerializeObject(roleModuleDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(roleUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(address,httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "success" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail" });
                }
            }
        }

        /// <summary>
        /// 根据角色id查询关联角色的权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowRoleModule(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                var rolemoduleDTO = new RoleModuleDTO();
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "ShowRoleModule?Id=" + Id);
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    rolemoduleDTO = JsonConvert.DeserializeObject<RoleModuleDTO>(result);
                }
                return View(rolemoduleDTO);
            }
        }
    }
}