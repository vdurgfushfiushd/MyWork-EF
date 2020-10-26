using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IGeneralDAL : IDisposable 
    {
        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="t"></param>
        void Add<T>(T t) where T : Entity;

        /// <summary>
        /// 多个对象新增
        /// </summary>
        /// <param name="list"></param>
        void AddRange<T>(IEnumerable<T> list) where T : Entity;

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="t"></param>
        void Remove<T>(T t) where T : Entity;

        /// <summary>
        /// 集合删除
        /// </summary>
        /// <param name="exp"></param>
        void RemoveRange<T>(IEnumerable<T> list) where T : Entity;

        /// <summary>
        /// 单个软删除
        /// </summary>
        /// <param name="t"></param>
        void MarkRemove<T>(T t) where T : Entity;

        /// <summary>
        /// 集合软删除
        /// </summary>
        /// <param name="exp"></param>
        void MarkRemoveRange<T>(IEnumerable<T> list) where T : Entity;

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <param name="t"></param>
        void Update<T>(T t) where T : Entity;

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="obj"></param>
        void Update<T>(T t, object obj) where T : Entity;

        /// <summary>
        /// 对象的状态改变（从Detached改变为Unchanged）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void Attach<T>(T t) where T : Entity;

        /// <summary>
        /// 单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        T GetEntity<T>(Expression<Func<T, bool>> exp) where T : Entity;

        /// <summary>
        /// 动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> GetFilter<T>(Expression<Func<T, bool>> exp) where T : Entity;

        /// <summary>
        /// 动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> GetFilterAsNoTracking<T>(Expression<Func<T, bool>> exp) where T : Entity;

        /// <summary>
        /// sql语句执行增删改操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteCommand<T>(string cmdText, params object[] parameters) where T : Entity;

        /// <summary>
        /// sql语句执行查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteQuery<T>(string cmdText, params object[] parameters) where T : Entity;

        /// <summary>
        /// 事务提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 事务提交(异步)
        /// </summary>
        Task CommitAsync();

    }
}
