using System;
using DTO;
using IBLL;
using IDAL;
using Model;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using AutoMapper.QueryableExtensions;

namespace BLL
{
    public class UserBLL : IUserBLL
    {
        public IGeneralDAL generalDAL { get; set; }

        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispose()
        {
            if (generalDAL != null)
            {
                generalDAL.Dispose();
            }
        }

        /// <summary>
        /// 单个用户新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(UserDTO userDTO)
        {
            //数据库中对应的用户
            var db_user = generalDAL.GetEntity<User>(e => e.Id == userDTO.Id && e.IsDeleted == false);
            if (db_user == null)
            {
                var user = Mapper.Map<User>(userDTO);
                user.Id = Guid.NewGuid().ToString("n");
                user.IsDeleted = false;
                user.CreateTime = DateTime.Now;
                generalDAL.Add(user);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个用户及其角色新增
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(UserDTO userDTO, string[] RoleIds)
        {
            //数据库中对应的用户
            var db_user = generalDAL.GetEntity<User>(e => e.Id == userDTO.Id && e.IsDeleted == false);
            if (db_user == null)
            {
                var user = Mapper.Map<User>(userDTO);
                user.Id = Guid.NewGuid().ToString("n");
                user.IsDeleted = false;
                user.CreateTime = DateTime.Now;
                generalDAL.Add(user);
                foreach (var RoleId in RoleIds)
                {
                    var db_role = generalDAL.GetEntity<Role>(e=>e.Id==RoleId&&e.IsDeleted==false);
                    user.Roles.Add(db_role);
                }
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个用户删除(软删除)
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(UserDTO userDTO)
        {
            //数据库中对应的用户
            var db_user = generalDAL.GetEntity<User>(e => e.Id == userDTO.Id && e.IsDeleted == false);
            //数据库中有数据，则可以删除
            if (db_user != null)
            {
                generalDAL.MarkRemove(db_user);
                await generalDAL.CommitAsync();
            } 
        }

        /// <summary>
        /// 动态条件用户删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(Expression<Func<User, bool>> exp)
        {
            //数据库中对应的用户
            var db_users = generalDAL.GetFilter(exp).Where(e=>e.IsDeleted == false);
            if (db_users.Any())
            {
                generalDAL.MarkRemoveRange(db_users);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个真实删除(真实删除)
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(UserDTO userDTO)
        {
            //数据库中对应的用户
            var db_user = generalDAL.GetEntity<User>(e=>e.Id==userDTO.Id&&e.IsDeleted==false);
            if (db_user != null)
            {
                db_user.Roles.Clear();
                generalDAL.Remove(db_user);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件真实删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<User, bool>> exp)
        {
            //用户表中对应的用户集合
            var db_users = generalDAL.GetFilter(exp).ToList();
            if (db_users.Any())
            {
                db_users.ForEach(e => {
                    e.Roles.Clear();
                });
                generalDAL.RemoveRange(db_users);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个用户修改
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="Names"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserDTO userDTO, string[] RoleIds)
        {
            var user = Mapper.Map<User>(userDTO);
            //数据库中要修改的user
            var db_user = generalDAL.GetEntity<User>(e => e.Id == userDTO.Id);
            //删除旧的user-role关系表数据
            db_user.Roles.Clear();
            //新增新的user-role关系表数据 
            foreach (var roleId in RoleIds)
            {
                var role = generalDAL.GetEntity<Role>(e => e.Id == roleId);
                db_user.Roles.Add(role);
            }
            generalDAL.Update(db_user,user);
            await generalDAL.CommitAsync();
        }

        /// <summary>
        /// 动态单个用户查找
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public UserDTO GetEntity(Expression<Func<User, bool>> exp)
        {
            var entity = generalDAL.GetEntity(exp);
            return Mapper.Map<UserDTO>(entity); 
        }

        /// <summary>
        /// 动态多个用户查找
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<UserDTO> GetFilter(Expression<Func<User, bool>> exp)
        {
            return generalDAL.GetFilter(exp).ProjectTo<UserDTO>().ToList();
        }

        /// <summary>
        /// 单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public UserDTO GetEntity(Dictionary<string, object> dict)
        { 
            var exp = Tool<User>.ToExpression(dict);
            var entity = generalDAL.GetEntity(exp);
            return Mapper.Map<UserDTO>(entity);
        }

        /// <summary>
        /// 动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<UserDTO> GetFilter(Dictionary<string, object> dict)
        {
            var exp = Tool<User>.ToExpression(dict);
            return generalDAL.GetFilter(exp).ProjectTo<UserDTO>().ToList();
        }

        /// <summary>
        /// 单个UserRoleDTO查找
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<UserRoleDTO> GetUpdateEntity(string Id)
        {
            try
            {
                //数据库中对应的用户
                var db_user = generalDAL.GetEntity<User>(e => e.Id == Id && e.IsDeleted == false);
                //用户对应的Role集合
                var db_roles = generalDAL.GetFilter<User>(e => e.Id == Id && e.IsDeleted == false).SelectMany(e=>e.Roles);
                //数据库中多有的角色集合
                var db_roles_all = generalDAL.GetFilter<Role>(e => e.IsDeleted == false);
                var result = (from db_role_all in db_roles_all
                              join db_role in db_roles on db_role_all.Id equals db_role.Id into _db_roles
                              from _db_role in _db_roles.DefaultIfEmpty()
                              select new UserRoleDTO()
                              {
                                  Id = db_user.Id,
                                  Name = db_user.Name,
                                  Password = db_user.Password,
                                  Height = db_user.Height,
                                  CreateTime = db_user.CreateTime,
                                  IsDeleted = db_user.IsDeleted,
                                  Describe = db_user.Describe,
                                  RoleId = db_role_all.Id,
                                  RoleName = db_role_all.Name,
                                  RoleDescribe = db_role_all.Describe,
                                  IsChoosed = (_db_role == null) ? false : true
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户所拥有的权限
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <returns></returns>
        public List<Module> GetUserModules(string Id)
        {
            return generalDAL.GetEntity<User>(e => e.Id == Id && e.IsDeleted == false).Roles.SelectMany(e=>e.Modules).ToList();
        }

        /// <summary>
        /// 获取用户所及其角色集合
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <returns></returns>
        public List<UserRoleDTO> GetUserRoles(string Id)
        {
            var db_user = generalDAL.GetEntity<User>(e => e.Id == Id && e.IsDeleted == false);
            return db_user.Roles.Select(e => new UserRoleDTO()
            {
                Id = Id,
                Name = db_user.Name,
                Password = db_user.Password,
                Height= db_user.Height,
                CreateTime= db_user.CreateTime,
                IsDeleted= db_user.IsDeleted,
                Describe= db_user.Describe,
                RoleId = e.Id,
                RoleName = e.Name,
                RoleDescribe = e.Describe,
            }).ToList();
        }

        /// <summary>
        /// 判断用户是否拥有该权限
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <param name="ControllerName">控制器名字</param>
        /// <param name="ActionName">action名字</param>
        /// <returns></returns>
        public bool GetPermissionFlag(string Id, string ControllerName, string ActionName)
        {
            //判断用户的模块是否拥有当前控制器名和action名对应的模块
            if (GetUserModules(Id).Where(e => e.ControllerName == ControllerName && e.ActionName == ActionName && e.IsDeleted == false).Any())
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取用户对应的角色，模块名字
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<UserRoleModuleDTO> GetUserRoleModule(string Id)
        {
            var db_user = generalDAL.GetEntity<User>(e => e.Id == Id && e.IsDeleted == false);
            return db_user.Roles.SelectMany(e=>e.Modules,(role,module)=> (role, module)).Select(e=>new UserRoleModuleDTO() {
                Id = Id,
                UserName = db_user.Name,
                Password = db_user.Password,
                RoleName = e.role.Name,
                ModuleName = e.module.ModuleName,
                ControllerName=e.module.ControllerName,
                ActionName=e.module.ActionName
            }).ToList();
        }
    }
}
