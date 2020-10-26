using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using DTO;
using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Module = Model.Module;

namespace BLL
{
    public class ModuleBLL:IModuleBLL
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
        /// 单个模块新增
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(ModuleDTO moduleDTO)
        {
            //数据库中对应的模块
            var db_module = generalDAL.GetEntity<Module>(e => e.Id == moduleDTO.Id && e.IsDeleted == false);
            if (db_module == null)
            {
                //要新增的数据
                var module = Mapper.Map<Module>(moduleDTO);
                module.Id = Guid.NewGuid().ToString("n");
                module.IsDeleted = false;
                module.CreateTime = DateTime.Now;
                generalDAL.Add(db_module);
                await generalDAL.CommitAsync();
            }
        }

        ///// <summary>
        ///// 单个模块新增
        ///// </summary>
        ///// <param name="moduleDTO"></param>
        ///// <returns></returns>
        //public async Task AddAsync(ModuleDTO moduleDTO,string[] ationNames)
        //{
        //    //数据库中对应的模块
        //    var db_module = generalDAL.GetEntity<Module>(e => e.Id == moduleDTO.Id && e.IsDeleted == false);
        //    if (db_module == null)
        //    {
        //        //要新增的数据
        //        var module = Mapper.Map<Module>(moduleDTO);
        //        module.Id = Guid.NewGuid().ToString("n");
        //        module.IsDeleted = false;
        //        module.CreateTime = DateTime.Now;
        //        generalDAL.Add(db_module);
        //        await generalDAL.CommitAsync();
        //    }
        //}

        /// <summary>
        /// 单个模块删除(软删除)
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(ModuleDTO moduleDTO)
        {
            //数据库中对应的模块
            var db_module = generalDAL.GetEntity<Module>(e => e.Id == moduleDTO.Id && e.IsDeleted == false);
            //设置模块的删除标志为true
            if (db_module != null)
            {
                generalDAL.MarkRemove(db_module);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件模块删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(Expression<Func<Module, bool>> exp)
        {
            //数据库中对应的模块
            var db_modules = generalDAL.GetFilter(exp).Where(e=>e.IsDeleted==false);
            if (db_modules.Any())
            {
                generalDAL.MarkRemoveRange(db_modules);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个模块删除(真实删除)
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ModuleDTO moduleDTO)
        {
            //数据库中对应的模块
            var db_module = generalDAL.GetEntity<Module>(e=>e.Id==moduleDTO.Id&&e.IsDeleted==false);
            if (db_module != null)
            {
                db_module.Roles.Clear();
                generalDAL.Remove(db_module);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件模块删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Module, bool>> exp)
        {
            //数据库中对应的模块
            var db_modules = generalDAL.GetFilter(exp).ToList();
            if (db_modules.Any())
            {
                db_modules.ForEach(e => {
                    //删除模块和角色对应的关系
                    e.Roles.Clear();
                });
                generalDAL.RemoveRange(db_modules);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 模块修改
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ModuleDTO moduleDTO, string[] actionNames)
        {
            //获取所有的ModuleId为module.ModuleId的模型类的集合
            var db_modules = generalDAL.GetFilter<Module>(e => e.Id == moduleDTO.Id && e.IsDeleted == false).ToList();
            generalDAL.RemoveRange(db_modules);
            var list = actionNames.Select(e => new Module()
            {
                Id = Guid.NewGuid().ToString("n"),
                CreateTime = DateTime.Now,
                IsDeleted = false,
                ActionName = e,
                ControllerName = moduleDTO.ControllerName,
                ModuleName = moduleDTO.ModuleName,
                Describe = moduleDTO.Describe,
            }).ToList();
            generalDAL.AddRange(list);
            //事务提交
            await generalDAL.CommitAsync();
        }

        /// <summary>
        /// 单个模块动态查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public ModuleDTO GetEntity(Expression<Func<Module, bool>> exp)
        {
            var entity = generalDAL.GetEntity(exp);
            return Mapper.Map<ModuleDTO>(entity);
        }

        /// <summary>
        /// 多个模块动态查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<ModuleDTO> GetFilter(Expression<Func<Module, bool>> exp)
        {
            return generalDAL.GetFilter(exp).ProjectTo<ModuleDTO>().ToList();
        }

        /// <summary>
        /// 动态获取单个模块
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public ModuleDTO GetEntity(Dictionary<string, object> dict)
        {
            var exp = Tool<Module>.ToExpression(dict);
            var entity = generalDAL.GetEntity(exp);
            return Mapper.Map<ModuleDTO>(entity);
        }

        /// <summary>
        /// 动态获取模块集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<ModuleDTO> GetFilter(Dictionary<string, object> dict)
        {
            var exp = Tool<Module>.ToExpression(dict);
            return generalDAL.GetFilter(exp).ProjectTo<ModuleDTO>().ToList();
        }

        /// <summary>
        /// 获取当前项目的所有的control
        /// </summary>
        /// <returns></returns>
        public List<string> GetControls()
        {
            List<string> controls = new List<string>();
            var asm = Assembly.Load("MyWork");
            List<Type> typeList = new List<Type>();
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                //去掉非自己创建的控制器
                if (type.Name.EndsWith("Controller"))
                {
                    if (type.Name.Equals("GeneralController"))
                        continue;
                    else
                        typeList.Add(type);
                }
            }
            typeList.Sort(delegate (Type type1, Type type2) { return type1.FullName.CompareTo(type2.FullName); });
            controls = typeList.Select(e => e.Name).ToList();
            return controls;
        }

        /// <summary>
        /// 获取当前项目中的指定的控制器所有的action
        /// </summary>
        /// <param name="ControlName">控制器名字</param>
        /// <returns></returns>
        public List<string> GetActions(string ControllerName)
        {
            if (!ControllerName.Contains("Controller"))
                ControllerName=ControllerName + "Controller";
            List<string> actions = new List<string>();
            var asm = Assembly.Load("MyWork");
            List<Type> typeList = new List<Type>();
            var x=asm.GetTypes();
            var types = asm.GetTypes().Where(e => e.Name == ControllerName);
            typeList.AddRange(types);
            typeList.Sort(delegate (Type type1, Type type2) { return type1.FullName.CompareTo(type2.FullName); });
            foreach (Type type in typeList)
            {
                System.Reflection.MemberInfo[] members = type.FindMembers(System.Reflection.MemberTypes.Method,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.NonPublic |        //【位屏蔽】
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly,
                Type.FilterName, "*");
                foreach (var m in members)
                {
                    if (m.DeclaringType.Attributes.HasFlag(System.Reflection.TypeAttributes.Public) != true)
                        continue;
                    string controller = type.Name.Replace("Controller", "");
                    string action = m.Name;
                    if (actions.Contains(action))
                    {
                        continue;
                    }
                    else
                    {
                        if (action.EndsWith("BLL"))
                            continue;
                        else
                            actions.Add(action);
                    }
                }
            }
            return actions;
        }

        /// <summary>
        /// 新增模块（单个）
        /// </summary>
        /// <param name="module"></param>
        /// <param name="ActionNames"></param>
        public async Task AddAsync(ModuleDTO moduleDTO, string[] ActionNames)
        {
            //数据库中该控制器名称对应的模块名的模块集合
            var db_modules = generalDAL.GetFilter<Module>(e => e.ControllerName == moduleDTO.ControllerName && e.IsDeleted == false);
            //如果数据库中有该控制器对应的模块，则抛出警告，否则新增
            if (!db_modules.Any())
            {
                var list = ActionNames.Select(e => new Module()
                {
                    Id = Guid.NewGuid().ToString("n"),
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    ActionName = e,
                    ControllerName = moduleDTO.ControllerName,
                    ModuleName = moduleDTO.ModuleName,
                    Describe = moduleDTO.Describe,
                });
                generalDAL.AddRange(list);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 获取要修改的module
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ModuleUpdateDTO GetUpdateModules(string Id)
        {
            //要修改的模块对象
            var module = generalDAL.GetEntity<Module>(e => e.Id == Id && e.IsDeleted == false);
            //module表中有的action
            var db_modules = generalDAL.GetFilter<Module>(e => e.ModuleName == module.ModuleName && e.IsDeleted == false).ToList();
            //控制器对应的所有的action
            var allActions = GetActions(module.ControllerName);
            var result = (from allAction in allActions
                          join db_module in db_modules on allAction equals db_module.ActionName into _db_modules
                          from _db_module in _db_modules.DefaultIfEmpty()
                          select new ModuleDTO()
                          {
                              ActionName = allAction,
                              IsChoosed = (_db_module == null) ? false : true
                          }).ToList();
            return new ModuleUpdateDTO() { ModuleDTO = Mapper.Map<ModuleDTO>(module), ModuleDTOs = result };
        }
    }
}
