using Common;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWorkByWebApi_V2.Controllers
{
    public class NoteController : Controller
    {
        
        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/Note/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/Note");

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = await WebApiHelper.GetAsync<List<NoteDTO>>($"{_baseAddress}/GetAll", null);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 日志新增
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(NoteDTO noteDTO)
        {
            var response= await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Add", noteDTO);
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
        /// 日志删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Delete", new { Id=Id});
            if (response.success)
            {
                return Redirect("/Note/Index");
            }
            else
            {
                return Redirect("/Erroe/Erroe");
            }
        }

        /// <summary>
        /// 日志修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetNote(string Id)
        {
            var noteDTO = await WebApiHelper.PostAsync<NoteDTO>($"{_baseAddress}/GetUpdateEntity", new { Id=Id});
            return View("Update", noteDTO);
        }

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(NoteDTO noteDTO)
        {
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Update", noteDTO);
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
        /// 我的日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MyNote()
        {
            var UserId = Session["UserId"].ToString();
            var list = await WebApiHelper.GetAsync<List<NoteDTO>>($"{_baseAddress}/GetFilter", new { UserId = UserId });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}