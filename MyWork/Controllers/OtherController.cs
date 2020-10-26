using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class OtherController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UnLogin()
        {
            return View();
        }
    }
}