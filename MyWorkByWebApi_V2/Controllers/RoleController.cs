using Common;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWorkByWebApi_V2.Controllers
{
    public class RoleController : Controller
    {
        
        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/Role/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/Role");

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = await WebApiHelper.GetAsync<List<RoleDTO>>($"{_baseAddress}/GetAll", null);
            return Json(list, JsonRequestBehavior.AllowGet);
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
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Add", roleDTO);
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
        /// 角色删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Delete", new { Id=Id});
            if (response.success)
            {
                return Redirect("/Role/Index");
            }
            else
            {
                return Redirect("/Error/Error");
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
            var rolemoduleDTO = await WebApiHelper.PostAsync<RoleModuleDTO>($"{_baseAddress}/GetUpdateEntity", new { Id = Id });
            return View("Update",rolemoduleDTO);
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
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Update", roleModuleDTO);
            if (response.success)
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "success" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail" });
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
            var rolemoduleDTO = await WebApiHelper.GetAsync<RoleModuleDTO>($"{_baseAddress}/ShowRoleModule", new { Id = Id });
            return View(rolemoduleDTO);
        }
    }
}