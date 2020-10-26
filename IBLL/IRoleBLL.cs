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
    /// <summary>
    /// 角色对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRoleBLL: IDisposable
    {
        /// <summary>
        /// 单个角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task AddAsync(RoleDTO roleDTO);

        /// <summary>
        /// 单个角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task AddAsync(RoleDTO roleDTO,string[] ModuleIds);

        /// <summary>
        /// 单个角色删除(软删除)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(RoleDTO roleDTO);
       
        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(Expression<Func<Role, bool>> exp);
      
        /// <summary>
        /// 单个角色删除
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(RoleDTO roleDTO);
       
        /// <summary>
        /// 动态条件删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Role, bool>> exp);

        /// <summary>
        /// 角色信息修改
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="moduleDTOs"></param>
        /// <returns></returns>
        Task UpdateAsync(RoleDTO roleDTO, string[] ModuleIds);
       
        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        RoleDTO GetEntity(Expression<Func<Role, bool>> exp);
        
        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<RoleDTO> GetFilter(Expression<Func<Role, bool>> exp);

        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        RoleDTO GetEntity(Dictionary<string,object> dict);

        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<RoleDTO> GetFilter(Dictionary<string, object> dict);

        /// <summary>
        /// 获取要修改的角色及其对应的模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<RoleModuleDTO> GetUpdateEntity(string Id);

        /// <summary>
        /// 获取指定角色及其模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<RoleModuleDTO> GetRoleModules(string Id);
    }
}
