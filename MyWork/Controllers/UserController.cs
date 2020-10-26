
using Common;
using DTO;
using IBLL;
using Model;
using MyWork.App_Start.Filter;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWork.Controllers
{
    public class UserController : Controller
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
        public ActionResult Index(string Name,DateTime? CreateTimeFrom,DateTime? CreateTimeTo,bool? IsDeleted)
        {
            Expression<Func<User, bool>> filter = m => true;
            if (!string.IsNullOrEmpty(Name))
                filter = filter.And(e=>e.Name==Name);
            if (CreateTimeFrom != null)
                filter = filter.And(e=>e.CreateTime>=CreateTimeFrom);
            if (CreateTimeTo != null)
                filter = filter.And(e => e.CreateTime <= CreateTimeTo);
            if(IsDeleted!=null)
                filter = filter.And(e => e.IsDeleted == IsDeleted);
            else
                filter = filter.And(e => e.IsDeleted == false);
            var list = userBLL.GetFilter(filter);
            return View(list);
        }

        /// <summary>
        /// 获取角色集合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRoles()
        {
            var result = roleBLL.GetFilter(e => true);
            return Json(result);
        }

        /// <summary>
        /// 用户新增界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Add(UserDTO userDTO)
        {
            await userBLL.AddAsync(userDTO);
            return Redirect("/User/Index");
        }

        /// <summary>
        /// 用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddByAdminister(UserDTO userDTO,string[] RoleIds)
        {
            await userBLL.AddAsync(userDTO, RoleIds);
            //return Redirect("/User/Index");
            return Content("新增成功");
        }

        /// <summary>
        /// 根据用户id删除指定的用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            await userBLL.DeleteAsync(e=>e.Id==Id);
            return Redirect("/User/Index");
        }

        /// <summary>
        /// 根据用户id删除指定的用户信息(软)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MaskDelete(string Id)
        {
            await userBLL.MaskDeleteAsync(e => e.Id == Id);
            return Redirect("/User/Index");
        }

        /// <summary>
        /// 根据用户id获取要修改的用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(string Id)
        {
            var result = userBLL.GetUpdateEntity(Id);
            return View(result);
        }

        /// <summary>
        /// 修改用户信息和关联表的数据
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="Role_Ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UserDTO userDTO,string[] RoleIds)
        {
            await userBLL.UpdateAsync(userDTO, RoleIds);
            return Redirect("/User/Index");
        }

        /// <summary>
        /// 根据用户id查询用户的所有关联角色名和权限名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowUserRole(string Id)
        {
            var list = userBLL.GetUserRoles(Id);
            return View(list);
        }

        /// <summary>
        /// 根据用户id查询用户的所有关联角色名和权限名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowUserRoleModule(string Id)
        {
            var list= userBLL.GetUserRoleModule(Id);
            return View(list);
        }
    }
}