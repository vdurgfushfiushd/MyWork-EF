using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RedisHelper<T> where T:class
    {

        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(T t)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"))
            {
                IDatabase db = redis.GetDatabase();//默认第一个数据库，
              
            }
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T t)
        {
           
        }

        /// <summary>
        /// 单个修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T t)
        {
           
        }

        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> exp)
        {
            return null;
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<T>> GetFiltersAsync(Expression<Func<T, bool>> exp)
        {
            return null;
        }

        /// <summary>
        /// 多条件删除 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<T, bool>> exp)
        {

        }
    }
}
