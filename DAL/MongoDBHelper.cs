using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MongoDBHelper<T> where T:class,IEntity
    {
        private MongoClient client;

        private IMongoDatabase database;

        private IMongoCollection<T> collection;

        public MongoDBHelper(string server, string DatabaseName, string CollectionName)
        {
            client = new MongoClient(server);
            database = client.GetDatabase(DatabaseName);
            collection = database.GetCollection<T>(CollectionName);
        }

        /// <summary>
        /// 单个对象新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(T t)
        {
            await collection.InsertOneAsync(t);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<T> list)
        {
            await collection.InsertManyAsync(list);
        }

        /// <summary>
        /// 根据条件动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp);
            await collection.DeleteManyAsync(filter);
        }

        /// <summary>
        /// 单个对象删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T t)
        {
            var filter = Builders<T>.Filter.Where(p => p.Id == t.Id);
            await collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T t)
        {
            //过滤要修改的
            var filter = Builders<T>.Filter.Where(p => p.Id == t.Id);
            var update = Builders<T>.Update.Set(p => p, t);
            await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// 根据Id查找单个
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(ObjectId Id)
        {
            var filter = Builders<T>.Filter.Eq(p => p.Id, (ObjectId)Id);
            var entity = await collection.Find<T>(filter).FirstOrDefaultAsync();
            return entity;
        }

        /// <summary>
        /// 动态条件单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp); ;
            var entity = await collection.Find<T>(filter).SingleOrDefaultAsync();
            return entity;
        }

        /// <summary>
        /// 动态条件多个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp);
            var entitys = await collection.Find<T>(filter).ToListAsync();
            return entitys;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="start"></param>
        /// <param name="pageCount"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<T>> PagingAsync(int start, int pageCount, Expression<Func<T, bool>> exp)
        {
            var findOpt = new FindOptions<T, T>();
            findOpt.Limit = pageCount;
            findOpt.Skip = (start - 1) * pageCount;
            var filter = Builders<T>.Filter.Where(exp);
            var result = await collection.FindAsync(filter, findOpt);
            return await result.ToListAsync();
        }
    }
}
