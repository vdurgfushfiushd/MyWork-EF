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
    public class MyWorkController : Controller
    {

        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/MyWork/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/MyWork/");

        /// <summary>
        /// 进入用户登录页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Login(UserDTO userDTO)
        {
            using (var httpClient = new HttpClient())
            {
                //要调用的方法地址
                Uri address = new Uri(_baseAddress, "Login");
                //将参数转换为字符串
                var userDTO_json = JsonConvert.SerializeObject(userDTO);
                //将传递的参数字符串转换为HttpContent类型
                HttpContent httpContent = new StringContent(userDTO_json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(address, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var user = JsonConvert.DeserializeObject<UserDTO>(result);
                    Session["UserName"] = user.Name;
                    Session["UserId"] = user.Id;
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail", Message = "没有此人" });
                }
            }
        }
    }
}