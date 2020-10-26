using Common;
using DTO;
using IBLL;
using Model;
using MyWork.App_Start.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class ModuleController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }
  
        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string ModuleName, string ControllerName,string ActionName, DateTime? CreateTimeFrom, DateTime? CreateTimeTo, bool? IsDeleted)
        {
            Expression<Func<Module, bool>> filter = m => true;
            if (!string.IsNullOrEmpty(ModuleName))
                filter = filter.And(e => e.ModuleName == ModuleName);
            if (!string.IsNullOrEmpty(ControllerName))
                filter = filter.And(e => e.ControllerName == ControllerName);
            if (!string.IsNullOrEmpty(ActionName))
                filter = filter.And(e => e.ActionName == ActionName);
            if (CreateTimeFrom != null)
                filter = filter.And(e => e.CreateTime >= CreateTimeFrom);
            if (CreateTimeTo != null)
                filter = filter.And(e => e.CreateTime <= CreateTimeTo);
            if (IsDeleted != null)
                filter = filter.And(e => e.IsDeleted == IsDeleted);
            else
                filter = filter.And(e => e.IsDeleted == false);
            var list = moduleBLL.GetFilter(filter);
            return View(list);
        }

        /// <summary>
        /// 获取控制器的集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetControllers()
        {
            var list = moduleBLL.GetFilter(e => true).Select(e => e.ControllerName).Distinct().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取指定控制器的action集合
        /// </summary>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetActions(string ControllerName)
        {
            var result= moduleBLL.GetActions(ControllerName);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetModules()
        {
            var list = moduleBLL.GetFilter(e => true).GroupBy(e => e.ModuleName).Select(e => e.FirstOrDefault().ModuleName);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var controls = moduleBLL.GetControls();
            return View(controls);
        }

        /// <summary>
        /// 模块新增
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddByAdminister(ModuleDTO moduleDTO,string[] ActionNames)
        {
            await moduleBLL.AddAsync(moduleDTO, ActionNames);
            //return Redirect("/Module/Index");
            return Content("新增成功");
        }

        /// <summary>
        /// 模块删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            await moduleBLL.DeleteAsync(e=>e.Id == Id);
            return Redirect("/Module/Index");
        }

        /// <summary>
        /// 模块删除(软删除)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MaskDelete(string Id)
        {
            await moduleBLL.MaskDeleteAsync(e => e.Id == Id);
            return Redirect("/Module/Index");
        }

        /// <summary>
        /// 模块修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(string Id)
        {
            var result = moduleBLL.GetUpdateModules(Id);
            return View(result);
        }

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(ModuleDTO moduleDTO,string[] actionNames)
        {
            await moduleBLL.UpdateAsync(moduleDTO, actionNames);
            return Redirect("/Module/Index");
        }

        /// <summary>
        /// 查看指定模块的详细内容
        /// </summary> 
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowModule(string Id)
        {
            var list = moduleBLL.GetFilter(e=>e.Id==Id);
            return View(list);
        }
    }
}