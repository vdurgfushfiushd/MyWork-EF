using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common;

namespace MyWorkByWebApi_V2.Controllers
{
    public class UserController : Controller
    {

        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/User/");

        public static readonly string _baseAddress = $"http://localhost:50681/api/User";

        [HttpGet]
        public ActionResult index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            //返回的结果
            var result =await WebApiHelper.GetAsync<List<UserDTO>>($"{_baseAddress}/GetAll",null);
            return Json(result, JsonRequestBehavior.AllowGet);
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
            //请求的返回值
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Add", userDTO);
            //http请求是否成功
            if (response.success)
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail" });
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
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Delete", new { Id= Id });
            //http请求是否成功
            if (response.success)
            {
                return Redirect("/User/Index");
            }
            else
            {
                return Redirect("/Error/Error");
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
            var userRoleDTO = await WebApiHelper.PostAsync<UserRoleDTO>($"{_baseAddress}/GetUpdateEntity", new { Id = Id });
            return View("Update",userRoleDTO);
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
            var response= await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Update", userUpdateDTO);
            if (response.success)
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail" });
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
            var userRoleModuleDTOs = await WebApiHelper.GetAsync<List<UserRoleModuleDTO>>($"{_baseAddress}/ShowUserRoleModule", new { Id= Id });
            return View(userRoleModuleDTOs);
        }
    }

   
}
