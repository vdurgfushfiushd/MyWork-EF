using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAL : IGeneralDAL
    {
        private MyDBContext ctx = MyContestFactory.GetMyDBContext();

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (this.ctx != null)
            {
                this.ctx.Dispose();
            }
        }

        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="t"></param>
        public void Add<T>(T t) where T : Entity
        {
            ctx.Set<T>().Add(t);
        }

        /// <summary>
        /// 多个对象新增
        /// </summary>
        /// <param name="list"></param>
        public void AddRange<T>(IEnumerable<T> list) where T : Entity
        {
            ctx.Set<T>().AddRange(list);
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="t"></param>
        public void Remove<T>(T t) where T : Entity
        {
            ctx.Set<T>().Remove(t);
        }

        /// <summary>
        /// 集合删除
        /// </summary>
        /// <param name="exp"></param>
        public void RemoveRange<T>(IEnumerable<T> list) where T : Entity
        {
            ctx.Set<T>().RemoveRange(list);
        }

        /// <summary>
        /// 单个软删除
        /// </summary>
        /// <param name="t"></param>
        public void MarkRemove<T>(T t) where T : Entity
        {
            t.IsDeleted = false;
        }

        /// <summary>
        /// 集合软删除
        /// </summary>
        /// <param name="exp"></param>
        public void MarkRemoveRange<T>(IEnumerable<T> list) where T : Entity
        {
            foreach (var t in list)
            {
                t.IsDeleted = false;
            }
        }

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : Entity
        {
            ctx.Set<T>().Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
        }

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="obj"></param>
        public void Update<T>(T t,object obj) where T : Entity
        {
            ctx.Entry<T>(t).CurrentValues.SetValues(obj);
        }

        /// <summary>
        /// 对象的状态改变（从Detached改变为Unchanged）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Attach<T>(T t) where T : Entity
        {
            ctx.Set<T>().Attach(t);
        }

        /// <summary>
        /// 单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public T GetEntity<T>(Expression<Func<T, bool>> exp) where T : Entity
        {
            return ctx.Set<T>().SingleOrDefault(exp);
        }

        /// <summary>
        /// 动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> GetFilter<T>(Expression<Func<T, bool>> exp) where T : Entity
        {
            return ctx.Set<T>().Where(exp);
        }

        /// <summary>
        /// 动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> GetFilterAsNoTracking<T>(Expression<Func<T, bool>> exp) where T : Entity
        {
            return ctx.Set<T>().AsNoTracking().Where(exp);
        }

        /// <summary>
        /// sql语句执行增删改操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteCommand<T>(string cmdText, params object[] parameters) where T : Entity
        {
            return ctx.Database.ExecuteSqlCommand(cmdText,parameters);
        }

        /// <summary>
        /// sql语句执行查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string cmdText, params object[] parameters) where T : Entity
        {
            return ctx.Database.SqlQuery<T>(cmdText,parameters);
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            ctx.SaveChanges();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public async Task CommitAsync()
        {
           await ctx.SaveChangesAsync();
        }
    }
}
