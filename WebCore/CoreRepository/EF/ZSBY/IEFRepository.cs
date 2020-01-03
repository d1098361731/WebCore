using Core;
using Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreRepository.EF
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity "></typeparam>
    public interface IEFRepository<TEntity, TPrimaryKey> where TEntity : class,IEntity<TPrimaryKey>
    {
        #region  同步方法
        IQueryable<TEntity> GetAll();

         IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression);



        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
         TEntity Frist(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
         TEntity Frist();

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
         TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
         TEntity FirstOrDefault();



         TEntity Insert(TEntity model);
         void InsertRange(List<TEntity> models);

         TPrimaryKey InsertAndGetId(TEntity model);

         void Delete(TEntity model);

         void Delete(TPrimaryKey Id);

         void DeleteRange(List<TEntity> models);



         TEntity Update(TEntity model);

#endregion

        #region  异步方法

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
          Task<TEntity> FristAsync(Expression<Func<TEntity, bool>> expression);

          Task<TEntity> FristAsync();

          Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

          Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
          Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 写入实体数据列表
        /// </summary>
        /// <param name="entity">实体类列表</param>
        /// <returns></returns>
         Task InsertRangeAsync(List<TEntity> entitys);
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
          Task UpdateAsync(TEntity entity);

        // <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
          Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
          Task DeleteByIdAsync(TPrimaryKey id);

        #endregion
    }
}
