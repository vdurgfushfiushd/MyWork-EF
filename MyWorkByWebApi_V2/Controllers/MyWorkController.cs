using Common;
using DTO;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWorkByWebApi_V2.Controllers
{
    public class MyWorkController : Controller
    {

        //public static readonly Uri _baseAddress = new Uri("http://127.0.0.1:8084/api/MyWork/");

        public static readonly Uri _baseAddress = new Uri("http://localhost:50681/api/MyWork");

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
            var response = await WebApiHelper.PostAsync<Response>($"{_baseAddress}/Login", userDTO);
            if (response.success)
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "success", Message = "" });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseDTO() { State = "fail", Message = "没有此人" });
            }
        }
    }
}