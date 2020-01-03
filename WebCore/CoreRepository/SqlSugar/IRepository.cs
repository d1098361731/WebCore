using Core;
using Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreRepository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity "></typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 获取对象所有数据
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(TEntity model);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Delete(TEntity model);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(TEntity model);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression);


        /// <summary>
        /// 根据id  获取数据
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<TEntity> QueryByIDAsync(object objId);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FristAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        /// 写入实体数据列表
        /// </summary>
        /// <param name="entity">实体类列表</param>
        /// <returns></returns>
        Task<bool> AddEnritysAsync(List<TEntity> entitys);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateEntitysAsync(List<TEntity> entitys);

        // <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(object id);

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(object[] ids);

        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        Task<ISugarQueryable<TEntity>> QueryAsync();

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        Task<ISugarQueryable<TEntity>> QueryAsync(string strWhere);

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        Task<ISugarQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<ISugarQueryable<TEntity>> QueryAsync(bool WheleIf, Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<ISugarQueryable<TEntity>> QueryAsync(bool WheleIf, Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc">true正序    否则倒序</param>
        /// <returns></returns>
        Task<ISugarQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression,
           Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
    }


}
