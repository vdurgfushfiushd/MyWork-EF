using Common;
using DTO;
using IBLL;
using Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 模块webapi控制器
    /// </summary>
    [RoutePrefix("api/Module/")]
    public class ModuleController : ApiController
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IModuleBLL moduleBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        /// <summary>
        /// 获取指定控制器的action集合
        /// </summary>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        [HttpPost]
        public List<string> GetActions(string ControlName)
        {
            return moduleBLL.GetActions(ControlName);
        }

        /// <summary>
        /// 获取模块集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ModuleDTO> GetAll()
        {
            return moduleBLL.GetFilter(e => e.IsDeleted == false).GroupBy(e => e.ModuleName).Select(e => e.FirstOrDefault()).ToList();
        }

        /// <summary>
        /// 根据动态条件获取单个模块对象
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public ModuleDTO GetEntity(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string, object>>();
            return moduleBLL.GetEntity(dict);
        }

        /// <summary>
        /// 根据动态条件获取模块对象集合
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public List<ModuleDTO> GetFilter(JObject jObject)
        {
            var dict = jObject.ToObject<Dictionary<string,object>>();
            return moduleBLL.GetFilter(dict);
        }

        /// <summary>
        /// 获取控制器的集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<string> GetControls()
        {
            return moduleBLL.GetControls();
        }

        /// <summary>
        /// 模块新增
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Add(JObject jObject)
        {
            ModuleUpdateDTO moduleUpdateDTO = jObject.ToObject<ModuleUpdateDTO>();
            ModuleDTO moduleDTO = moduleUpdateDTO.ModuleDTO;
            string[] actionNames = moduleUpdateDTO.ModuleDTOs.Select(e=>e.ActionName).ToArray();
            await moduleBLL.AddAsync(moduleDTO, actionNames);
            return new Response(true);
        }

        /// <summary>
        /// 模块删除
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Delete(JObject jObject)
        {
            string Id = jObject["Id"].ToString();
            await moduleBLL.DeleteAsync(e => e.Id == Id&&e.IsDeleted==false);
            return new Response(true);
        }

        /// <summary>
        /// 获取要修改的模块对象
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public ModuleUpdateDTO GetUpdateEntity(JObject jObject)
        {
            string ModuleId = jObject["ModuleId"].ToString();
            return moduleBLL.GetUpdateModules(ModuleId);
        }

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> Update(JObject jObject)
        {
            ModuleUpdateDTO moduleUpdateDTO = jObject.ToObject<ModuleUpdateDTO>();
            ModuleDTO moduleDTO = moduleUpdateDTO.ModuleDTO;
            string[] actionNames = moduleUpdateDTO.ModuleDTOs.Select(e=>e.ActionName).ToArray();
            await moduleBLL.UpdateAsync(moduleDTO, actionNames);
            return new Response(true);
        }

        /// <summary>
        /// 查看指定模块的详细内容
        /// </summary> 
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ModuleDTO> ShowModule(string Id)
        {
            return moduleBLL.GetFilter(e => e.Id == Id && e.IsDeleted == false);
        }
    }
}
