using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 用户对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUserBLL:IDisposable
    {
        /// <summary>
        /// 单个用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task AddAsync(UserDTO userDTO);

        /// <summary>
        /// 单个用户及其角色新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task AddAsync(UserDTO userDTO,string[] RoleIds);


        /// <summary>
        /// 单个删除(软删除)
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(UserDTO userDTO);


        /// <summary>
        /// 动态条件单个删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(Expression<Func<User, bool>> exp);


        /// <summary>
        /// 单个真实删除
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(UserDTO userDTO);


        /// <summary>
        /// 动态条件真实新增
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<User, bool>> exp);


        /// <summary>
        /// 单个用户信息修改
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="RoleIds"></param>
        /// <returns></returns>
        Task UpdateAsync(UserDTO userDTO, string[] RoleIds);


        /// <summary>
        /// 单个用户动态查找
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        UserDTO GetEntity(Expression<Func<User, bool>> exp);


        /// <summary>
        /// 多个动态查找
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<UserDTO> GetFilter(Expression<Func<User, bool>> exp);

        /// <summary>
        /// 单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        UserDTO GetEntity(Dictionary<string, object> dict);

        /// <summary>
        /// 动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<UserDTO> GetFilter(Dictionary<string, object> dict);


        /// <summary>
        /// UserRoleDTO查找
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<UserRoleDTO> GetUpdateEntity(string Id);

        /// <summary>
        /// 判断用户是否拥有该权限
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <param name="ControllerName">控制器名字</param>
        /// <param name="ActionName">action名字</param>
        /// <returns></returns>
        bool GetPermissionFlag(string Id, string ControllerName, string ActionName);

        /// <summary>
        /// 获取用户对应的角色，模块集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<UserRoleModuleDTO> GetUserRoleModule(string Id);

        /// <summary>
        /// 获取用户及其对应的角色集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<UserRoleDTO> GetUserRoles(string Id);

    }
}
