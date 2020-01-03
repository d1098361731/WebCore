using Core;
using Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebEf.DbContext;

namespace CoreRepository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public SugarDbContext DbContext { get; set; }

        public SqlSugarClient Client { get; set; }

        public SimpleClient<TEntity> EntityDb { get; set; }

        public BaseRepository()
        {
            DbContext = SugarDbContext.GetDbContext();
            Client = DbContext.Db;
            
             EntityDb = DbContext.GetEntityDB<TEntity>(Client);
        }


        #region   同步方法
        public int Add(TEntity model)
        {
            
            return Client.Insertable<TEntity>(model).ExecuteCommand();
        }

        public bool Delete(TEntity model)
        {
            int index = Client.Deleteable<TEntity>(model).ExecuteCommand();
            if (index > 0)
                return true;
            return false;
        }

        public List<TEntity> GetAll()
        {

            return Client.GetSimpleClient<TEntity>().GetList();
        }

        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Client.GetSimpleClient<TEntity>().GetList(whereExpression);
        }


        public int Update(TEntity model)
        {
            return Client.Updateable<TEntity>(model).ExecuteCommand();
        }

        #endregion

        #region  异步方法
        /// <summary>
        /// 根据id  获取数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryByIDAsync(object objId)
        {
            return await Client.Queryable<TEntity>().InSingleAsync(objId);
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TEntity> FristAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Client.Queryable<TEntity>().FirstAsync(expression);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(TEntity entity)
        {
            return await Client.Insertable(entity).ExecuteCommandIdentityIntoEntityAsync();
        }

        /// <summary>
        /// 写入实体数据列表
        /// </summary>
        /// <param name="entity">实体类列表</param>
        /// <returns></returns>
        public async Task<bool> AddEnritysAsync(List<TEntity> entitys)
        {
            if (entitys != null && entitys.Count > 0)
                return await Task.Run(() => Client.Insertable(entitys).ExecuteCommand() >= entitys.Count);
            return false;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            //这种方式会以主键为条件
            return await Client.Updateable(entity).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> UpdateEntitysAsync(List<TEntity> entitys)
        {
            if (entitys != null && entitys.Count > 0)
                return await Task.Run(() => Client.Updateable(entitys).ExecuteCommand() >= entitys.Count);
            return false;
        }

        // <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var i = await Client.Deleteable(entity).ExecuteCommandAsync();
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(object id)
        {
            var i = await Client.Deleteable<TEntity>(id).ExecuteCommandAsync();
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(object[] ids)
        {
            var i = await Client.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
            return i > 0;
        }

        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<ISugarQueryable<TEntity>> QueryAsync()
        {
            return await Task.Run(() => Client.Queryable<TEntity>());
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<ISugarQueryable<TEntity>> QueryAsync(string strWhere)
        {
            return await Task.Run(() => Client.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere));
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<ISugarQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => Client.Queryable<TEntity>().Where(whereExpression));
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<ISugarQueryable<TEntity>> QueryAsync(bool WheleIf, Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => Client.Queryable<TEntity>().WhereIF(WheleIf, whereExpression));
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<ISugarQueryable<TEntity>> QueryAsync(bool WheleIf, Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => Client.Queryable<TEntity>().WhereIF(WheleIf, whereExpression).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds));
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc">true正序    否则倒序</param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => Client.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression)
            .OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc));
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(ISugarQueryable<TEntity> quey, string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await quey.WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .ToPageListAsync(intPageIndex, intPageSize);
        }




        public async Task<List<TEntity>> QueryPageAsync(ISugarQueryable<TEntity> quey, Expression<Func<TEntity, bool>> whereExpression,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null, bool isAsc = true)
        {
            return await quey
            .WhereIF(whereExpression != null, whereExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .ToPageListAsync(intPageIndex, intPageSize);
        }

        #endregion




    }



}
