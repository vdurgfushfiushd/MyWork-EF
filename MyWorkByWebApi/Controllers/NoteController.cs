using DTO;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWorkByWebApi.Controllers
{
    public class NoteController : Controller
    {
        
        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/Note/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/Note/");

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<Note>();
                Uri address = new Uri(_baseAddress, "GetAll");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Note>>(result);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
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
            using (var httpClient = new HttpClient())
            {
                noteDTO.UserId = Session["UserId"].ToString();
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "Add");
                //将参数转换为字符串
                var noteDTO_json = JsonConvert.SerializeObject(noteDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(noteDTO_json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(address, httpContent);
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
        /// 日志删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<Note>();
                Uri address = new Uri(_baseAddress, "Delete");
                var userUpdateDTOjson = JsonConvert.SerializeObject(Id);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Note/Index");
                }
                else
                {
                    return Redirect("/Erroe/Erroe");
                }
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
            using (var httpClient = new HttpClient())
            {
                NoteDTO noteDTO = new NoteDTO();
                Uri address = new Uri(_baseAddress, "GetEntity");
                var userUpdateDTOjson = JsonConvert.SerializeObject(Id);
                HttpContent httpContent = new StringContent(userUpdateDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    noteDTO = JsonConvert.DeserializeObject<NoteDTO>(result);
                }
                return View(noteDTO);
            }
        }

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(NoteDTO noteDTO)
        {
            using (var httpClient = new HttpClient())
            {
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "Update");
                //将参数转换为字符串
                var noteDTO_json = JsonConvert.SerializeObject(noteDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(noteDTO_json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(address, httpContent);
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
        /// 我的日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MyNote()
        {

            using (var httpClient = new HttpClient())
            {
                var UserId = Session["UserId"].ToString();
                List<NoteDTO> list = new List<NoteDTO>();
                Uri address = new Uri(_baseAddress, "GetFilter?UserId=" + UserId);
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<NoteDTO>>(result);
                }
                return View(list);
            }
        }
    }
}