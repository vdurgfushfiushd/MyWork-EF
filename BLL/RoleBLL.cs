using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleBLL:IRoleBLL
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
        /// 单个角色新增(异步)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(RoleDTO roleDTO)
        {
            //数据库中的角色数据
            var db_role = generalDAL.GetEntity<Role>(e => e.Id == roleDTO.Id && e.IsDeleted == false);
            if (db_role == null)
            {
                var role = Mapper.Map<Role>(roleDTO);
                role.Id = Guid.NewGuid().ToString("n");
                role.IsDeleted = false;
                role.CreateTime = DateTime.Now;
                generalDAL.Add(role);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个角色新增
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(RoleDTO roleDTO, string[] ModuleIds)
        {
            //数据库中的角色数据
            var db_role = generalDAL.GetEntity<Role>(e => e.Id == roleDTO.Id && e.IsDeleted == false);
            if (db_role == null)
            {
                var role = Mapper.Map<Role>(roleDTO);
                role.Id = Guid.NewGuid().ToString("n");
                role.IsDeleted = false;
                role.CreateTime = DateTime.Now;
                foreach (var ModuleId in ModuleIds)
                { 
                    var db_module= generalDAL.GetEntity<Module>(e => e.Id == ModuleId && e.IsDeleted == false);
                    role.Modules.Add(db_module);
                }
                generalDAL.Add(role);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个删除角色(软删除)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(RoleDTO roleDTO)
        {
            //数据库中的角色数据
            var db_role = generalDAL.GetEntity<Role>(e=>e.Id==roleDTO.Id&&e.IsDeleted==false);
            if (db_role != null)
            {
                generalDAL.MarkRemove(db_role);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(Expression<Func<Role, bool>> exp)
        {
            //数据库中的角色数据
            var db_roles = generalDAL.GetFilter(exp).Where(e=>e.IsDeleted == false);
            if (db_roles.Any())
            {
                generalDAL.MarkRemoveRange(db_roles);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个角色删除(真实删除)
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(RoleDTO roleDTO)
        {
            //数据库中对应的角色
            var db_role = generalDAL.GetEntity<Role>(e=>e.Id==roleDTO.Id&&e.IsDeleted==false);
            if (db_role != null)
            {
                //删除角色用户关系表中的数据
                db_role.Users.Clear();
                //删除角色模块关系表中的数据
                db_role.Modules.Clear();
                generalDAL.Remove(db_role);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态删除角色
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Role, bool>> exp)
        {
            //数据库中的角色集合
            var db_roles = generalDAL.GetFilter(exp).ToList();
            if (db_roles.Any())
            {
                db_roles.ForEach(e => {
                    //删除角色用户关系表中的数据
                    e.Users.Clear();
                    //删除角色模块关系表中的数据
                    e.Modules.Clear();
                });
                generalDAL.RemoveRange(db_roles);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 修改角色数据
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="DescriptionIds"></param>
        /// <returns></returns>
        public async Task UpdateAsync(RoleDTO roleDTO,string[] ModuleIds)
        {
            var role = Mapper.Map<Role>(roleDTO);
            //数据库中要修改的role
            var db_role = generalDAL.GetEntity<Role>(e=>e.Id== roleDTO.Id&&e.IsDeleted==false);
            //删除role-module关系表中的数据
            db_role.Modules.Clear();
            if (ModuleIds!=null)
            {
                //新增role-module关系表中的数据
                foreach (var ModuleId in ModuleIds)
                {
                    var module = generalDAL.GetEntity<Module>(e => e.Id == ModuleId && e.IsDeleted == false);
                    db_role.Modules.Add(module);
                }
            }
            generalDAL.Update(db_role,role);
            await generalDAL.CommitAsync();
        }

        /// <summary>
        /// 动态获取单个角色对象
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public RoleDTO GetEntity(Expression<Func<Role, bool>> exp)
        {
            var role = generalDAL.GetEntity(exp);
            return Mapper.Map<RoleDTO>(role);
        }
        
        /// <summary>
        /// 动态角色集合查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<RoleDTO> GetFilter(Expression<Func<Role, bool>> exp)
        {
            return generalDAL.GetFilter(exp).ProjectTo<RoleDTO>().ToList();
        }

        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public RoleDTO GetEntity(Dictionary<string, object> dict)
        {
            var exp = Tool<Role>.ToExpression(dict);
            var role = generalDAL.GetEntity(exp);
            return Mapper.Map<RoleDTO>(role);
        }

        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<RoleDTO> GetFilter(Dictionary<string, object> dict)
        {
            var exp = Tool<Role>.ToExpression(dict);
            return generalDAL.GetFilter(exp).ProjectTo<RoleDTO>().ToList();
        }

        /// <summary>
        /// 获取要修改的角色及其模型集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<RoleModuleDTO> GetUpdateEntity(string RoleId)
        {
            //数据库中的对应的角色对象
            var db_role = generalDAL.GetEntity<Role>(e => e.Id == RoleId && e.IsDeleted == false);
            //角色对应的module集合
            var db_modules = generalDAL.GetFilter<Role>(e => e.Id == RoleId && e.IsDeleted == false).SelectMany(e=>e.Modules);
            //数据库当中所有的module集合
            var db_modules_all = generalDAL.GetFilter<Module>(e => e.IsDeleted == false);
            var result= (from db_moduleall in db_modules_all
                    join db_module in db_modules on db_moduleall.Id equals db_module.Id into _db_modules
                    from _db_module in _db_modules.DefaultIfEmpty()
                    select new RoleModuleDTO()
                    {
                        Id = db_role.Id,
                        Name = db_role.Name,
                        CreateTime = db_role.CreateTime,
                        IsDeleted = db_role.IsDeleted,
                        Describe = db_role.Describe,
                        ModuleId = db_moduleall.Id,
                        ModuleName = db_moduleall.ModuleName,
                        ModuleDescribe = db_moduleall.Describe,
                        ControllerName = db_moduleall.ControllerName,
                        ActionName = db_moduleall.ActionName,
                        IsChoosed = (_db_module == null) ? false : true
                    }).ToList();
            return result;
        }

        /// <summary>
        /// 获取指定角色及其模块集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<RoleModuleDTO> GetRoleModules(string RoleId)
        {
            //获取指定的角色
            var db_role = generalDAL.GetEntity<Role>(e=>e.Id== RoleId && e.IsDeleted==false);
            return db_role.Modules.Select(e=>new RoleModuleDTO() {
                Id = db_role.Id,
                Name= db_role.Name,
                CreateTime= db_role.CreateTime,
                IsDeleted= db_role.IsDeleted,
                Describe= db_role.Describe,
                ModuleId=e.Id,
                ModuleName=e.ModuleName,
                ModuleDescribe=e.Describe,
                ControllerName=e.ControllerName,
                ActionName=e.ActionName
            }).ToList();
        }
    }
}
