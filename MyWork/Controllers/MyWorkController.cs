using DTO;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class MyWorkController : Controller
    {
        public IUserBLL userBLL { get; set; }

        // GET: MyWork
        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult Login(UserDTO userDTO)
        {
            var user = userBLL.GetEntity(e => e.Name == userDTO.Name && e.Password == userDTO.Password && e.IsDeleted == false);
            if (user != null)
            {
                Session["UserName"] = user.Name;
                Session["UserId"] = user.Id;
                return Redirect("/MyWork/Index");
            }
            else
            {
                return Content("没有此人");
            }
        }
    }
}