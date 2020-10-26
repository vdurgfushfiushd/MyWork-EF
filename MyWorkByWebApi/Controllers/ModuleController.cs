 using DTO;
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
    public class ModuleController : Controller
    {

        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/Module/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/Module/");

        /// <summary>
        /// 获取指定控制器的action集合
        /// </summary>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetActions(string ControlName)
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<string>();
                Uri address = new Uri(_baseAddress, "GetActions");
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(ControlName, Encoding.UTF8, "application/json");
                var response=await httpClient.PostAsync(address, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<string>>(result);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<ModuleDTO>();
                Uri address = new Uri(_baseAddress, "GetAll");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<ModuleDTO>>(result).GroupBy(e => e.ModuleName).Select(e => e.FirstOrDefault()).ToList();
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<string>();
                Uri address = new Uri(_baseAddress, "GetControls");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<string>>(result);
                }
                return View(list);
            }
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
            using (var httpClient = new HttpClient())
            {
                var list = new List<string>();
                Uri address = new Uri(_baseAddress, "GetActions");
                string moduleAddDTOjson = JsonConvert.SerializeObject(new ModuleUpdateDTO() { ModuleDTO=moduleDTO,ModuleDTOs= actionNames.Select(e=>new ModuleDTO() { ActionName=e}).ToList() });
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(moduleAddDTOjson, Encoding.UTF8, "application/json");
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
        /// 模块删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(string ModuleId)
        {
            using (var httpClient = new HttpClient())
            {
                var list = new List<string>();
                Uri address = new Uri(_baseAddress, "Delete");
                var moduleDTOjson = JsonConvert.SerializeObject(ModuleId);
                HttpContent httpContent = new StringContent(moduleDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/Module/Index");
                }
                else
                {
                    return Redirect("/Error/Error");
                }
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
            using (var httpClient = new HttpClient())
            {
                ModuleUpdateDTO moduleViewDTO = new ModuleUpdateDTO();
                Uri address = new Uri(_baseAddress, "GetUpdateEntity");
                var moduleDTOjson = JsonConvert.SerializeObject(ModuleId);
                HttpContent httpContent = new StringContent(moduleDTOjson, Encoding.UTF8, "application/json");
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    moduleViewDTO = JsonConvert.DeserializeObject<ModuleUpdateDTO>(result);
                }
                return View(moduleViewDTO);
            }
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
            using (var httpClient = new HttpClient())
            {
                var list = new List<string>();
                Uri address = new Uri(_baseAddress, "Update");
                string moduleUpdateDTOjson = JsonConvert.SerializeObject(moduleUpdateDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(moduleUpdateDTOjson, Encoding.UTF8, "application/json");
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
        /// 查看指定模块的详细内容
        /// </summary> 
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ShowModule(string ModuleId)
        {
            using (var httpClient = new HttpClient())
            {
                var moduleDTOs = new List<ModuleDTO>();
                Uri address = new Uri(_baseAddress, "ShowModule?ModuleId=" + ModuleId);
                var response = await httpClient.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    moduleDTOs = JsonConvert.DeserializeObject<List<ModuleDTO>>(result);
                }
                return View(moduleDTOs);
            }
        }
    }
}