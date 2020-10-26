using Common;
using DTO;
using IBLL;
using Model;
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
    public class NoteController : Controller
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
        public ActionResult Index(string Name,string Author,DateTime? CreateTimeFrom,DateTime? CreateTimeTo,bool? IsDeleted)
        {
            Expression<Func<Note, bool>> filter = m => true;
            if (!string.IsNullOrEmpty(Name))
                filter = filter.And(e => e.Name == Name);
            if (!string.IsNullOrEmpty(Author))
                filter = filter.And(e => e.User.Name == Author);
            if (CreateTimeFrom != null)
                filter = filter.And(e => e.CreateTime >= CreateTimeFrom);
            if (CreateTimeTo != null)
                filter = filter.And(e => e.CreateTime <= CreateTimeTo);
            if (IsDeleted != null)
                filter = filter.And(e => e.IsDeleted == IsDeleted);
            else
                filter = filter.And(e => e.IsDeleted == false);
            var list = noteBLL.GetFilter(filter);
            return View(list);
        }

        [HttpGet]
        public JsonResult GetNotes()
        {
            var list = noteBLL.GetNotes(e => true);
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
        public async Task<ActionResult> Add(NoteDTO noteDTO)
        {
            noteDTO.UserId = Session["UserId"].ToString();
            await noteBLL.AddAsync(noteDTO);
            //return Redirect("/Note/Index");
            return Content("新增成功");
        }

        /// <summary>
        /// 日志删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            await noteBLL.DeleteAsync(e=>e.Id==Id);
            return Redirect("/Note/Index");
        }

        /// <summary>
        /// 日志删除(软删除)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MaskDelete(string Id)
        {
            await noteBLL.MaskDeleteAsync(e => e.Id == Id);
            return Redirect("/Note/Index");
        }

        /// <summary>
        /// 日志修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(string Id)
        {
            var note = noteBLL.GetEntity(e=>e.Id==Id);
            return View(note);
        }

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(NoteDTO noteDTO)
        {
            await noteBLL.UpdateAsync(noteDTO);
            return Redirect("/Note/Index");
        }

        /// <summary>
        /// 我的日志
        /// </summary>
        /// <returns></returns>
        public ActionResult MyNote()
        {
            var UserId=Session["UserId"].ToString();
            var list = noteBLL.GetFilter(e=>e.UserId== UserId);
            return View(list);
        }
    }
}