using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IModuleBLL: IDisposable
    {
        /// <summary>
        /// 单个模块新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task AddAsync(ModuleDTO roleDTO);

        /// <summary>
        /// 单个删除模块
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(ModuleDTO roleDTO);

        /// <summary>
        /// 动态删除模块
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(Expression<Func<Module, bool>> exp);

        /// <summary>
        /// 单个删除模块
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(ModuleDTO roleDTO);

        /// <summary>
        /// 动态删除模块
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Module, bool>> exp);

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        Task UpdateAsync(ModuleDTO module, string[] actionNames);

        /// <summary>
        /// 动态获取单个模块
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        ModuleDTO GetEntity(Expression<Func<Module, bool>> exp);

        /// <summary>
        /// 动态获取模块集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<ModuleDTO> GetFilter(Expression<Func<Module, bool>> exp);

        /// <summary>
        /// 动态获取单个模块
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        ModuleDTO GetEntity(Dictionary<string,object> dict);

        /// <summary>
        /// 动态获取模块集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<ModuleDTO> GetFilter(Dictionary<string, object> dict);

        /// <summary>
        /// 获取当前项目的所有的control
        /// </summary>
        /// <returns></returns>
        List<string> GetControls();

        /// <summary>
        /// 获取当前项目中的所有的action
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        List<string> GetActions(string ControlName);

        /// <summary>
        /// 模型新增
        /// </summary>
        /// <param name="module"></param>
        /// <param name="ActionNames"></param>
        Task AddAsync(ModuleDTO moduleDTO, string[] ActionNames);

        /// <summary>
        /// 获取要修改的module
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        ModuleUpdateDTO GetUpdateModules(string ModuleId);

    }
}
