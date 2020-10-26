using Common;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWorkByWebApi_V2.Controllers
{
    public class ModuleController : Controller
    {

        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/Module/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/Module");

        /// <summary>
        /// 获取指定控制器的action集合
        /// </summary>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetActions(string ControlName)
        {
            var list = await WebApiHelper.GetAsync<List<string>>($"{_baseAddress}/GetActions", new { ControlName = ControlName });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = await WebApiHelper.GetAsync<List<ModuleDTO>>($"{_baseAddress}/GetAll", null);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var list = await WebApiHelper.GetAsync<List<string>>($"{_baseAddress}/GetControls", null);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 模块新增
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(ModuleDTO moduleDTO,string[] actionNames)
        {
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Add", new { ModuleDTO = moduleDTO, ActionNames = actionNames });
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
        /// 模块删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string ModuleId)
        {
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Delete", new { ModuleId= ModuleId });
            if (response.success)
            {
                return Redirect("/Module/Index");
            }
            else
            {
                return Redirect("/Error/Error");
            }
        }

        /// <summary>
        /// 模块修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetModule(string ModuleId)
        {
            var moduleViewDTO = await WebApiHelper.PostAsync<ModuleUpdateDTO>($"{_baseAddress}/GetUpdateEntity", new { ModuleId = ModuleId });
            return View("Update", moduleViewDTO);
        }

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(ModuleUpdateDTO moduleUpdateDTO)
        {
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Update", moduleUpdateDTO);
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
        /// 查看指定模块的详细内容
        /// </summary> 
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowModule(string ModuleId)
        {
            var moduleDTOs = await WebApiHelper.GetAsync<ModuleUpdateDTO>($"{_baseAddress}/ShowModule", new { ModuleId = ModuleId });
            return View(moduleDTOs);
        }
    }
}