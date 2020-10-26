using Common;
using DTO;
using IBLL;
using Model;
using MyWork.App_Start.Filter;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class RoleController : Controller
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
        public ActionResult Index(string Name, DateTime? CreateTimeFrom, DateTime? CreateTimeTo, bool? IsDeleted)
        {
            Expression<Func<Role, bool>> filter = m => true;
            if (!string.IsNullOrEmpty(Name))
                filter = filter.And(e => e.Name == Name);
            if (CreateTimeFrom != null)
                filter = filter.And(e => e.CreateTime >= CreateTimeFrom);
            if (CreateTimeTo != null)
                filter = filter.And(e => e.CreateTime <= CreateTimeTo);
            if (IsDeleted != null)
                filter = filter.And(e => e.IsDeleted == IsDeleted);
            else
                filter = filter.And(e => e.IsDeleted == false);
            var list = roleBLL.GetFilter(e => e.IsDeleted == false);
            return View(list);
        }

        /// <summary>
        /// 获取所有的模块集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetModules()
        {
            var list = moduleBLL.GetFilter(e => true).GroupBy(e=>e.ControllerName).Select(e=>e.FirstOrDefault()).ToList();
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
        public async Task<ActionResult> Add(RoleDTO roleDTO,string[] moduleIds)
        {
            await roleBLL.AddAsync(roleDTO, moduleIds);
            return Redirect("/Role/Index");
        }

        /// <summary>
        /// 角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddByAdminister(RoleDTO roleDTO,string[] ModuleIds)
        {
            await roleBLL.AddAsync(roleDTO, ModuleIds);
            //return Redirect("/Role/Index");
            return Content("新增成功");
        }

        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            await roleBLL.DeleteAsync(e=>e.Id==Id);
            return Redirect("/Role/Index");
        }

        /// <summary>
        /// 角色删除(软删除)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MaskDelete(string Id)
        {
            await roleBLL.MaskDeleteAsync(e => e.Id == Id);
            return Redirect("/Role/Index");
        }

        /// <summary>
        /// 角色修改界面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(string Id)
        {
            var result = roleBLL.GetUpdateEntity(Id);
            return View(result);
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="Module_Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(RoleDTO roleDTO,string[] ModuleIds)
        {
            await roleBLL.UpdateAsync(roleDTO, ModuleIds);
            return Redirect("/Role/Index");
        }

        /// <summary>
        /// 根据角色id查询关联角色的权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowRoleModule(string Id)
        {
            var result = roleBLL.GetRoleModules(Id);
            return View(result);
        }
    }
}