using Core;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebEf;

namespace CoreRepository.EF
{
    public class EFBaseRepository<TEntity, TPrimaryKey> : IEFRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual EFDbContext DbContext { get; set; }
        private DbSet<TEntity> Table { get; set; }

        public EFBaseRepository(EFDbContext dbContext)
        {
            this.DbContext = dbContext;
            if (DbContext == null)
                DbContext = new EFDbContext();
            Table = DbContext.Set<TEntity>();
        }


        #region   同步方法

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Table.Where(whereExpression);
        }

      

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity Frist(Expression<Func<TEntity, bool>> expression)
        {
            return Table.First<TEntity>(expression);
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity Frist()
        {
            return Table.First<TEntity>();
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Table.FirstOrDefault<TEntity>(expression);
        }

        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault()
        {
            return Table.FirstOrDefault<TEntity>();
        }



        public TEntity Insert(TEntity model)
        {
           return  Table.Add(model)?.Entity;
        }

        public void  InsertRange(List<TEntity> models)
        {
             Table.AddRange(models);
        }

        public TPrimaryKey InsertAndGetId(TEntity model)
        {
            var entity = Insert(model);
            if (entity != null)
            {
                DbContext.SaveChanges();
                return entity.Id;
            }
         return default(TPrimaryKey);
        }

        public void  Delete(TEntity model)
        {
            Table.Remove(model);
        }

        public void Delete(TPrimaryKey Id)
        {
            var  model = Table.FirstOrDefault(x => x.Id.Equals(Id));
            if(model!=null)
                Table.Remove(model);
        }

        public void DeleteRange(List<TEntity> models)
        {
            Table.RemoveRange(models);
        }

        

        public TEntity Update(TEntity model)
        {
            DbContext.Entry<TEntity>(model).State = EntityState.Modified;
            return model;
        }

        #endregion

        #region  异步方法
       
        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TEntity> FristAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Table.FirstAsync(expression);
        }

        public async Task<TEntity> FristAsync()
        {
            return await Table.FirstAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await Table.FirstOrDefaultAsync(expression);

            }catch(Exception ex)
            {
                return null;
            }
            
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Table.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(Insert(entity));
        }

        /// <summary>
        /// 写入实体数据列表
        /// </summary>
        /// <param name="entity">实体类列表</param>
        /// <returns></returns>
        public async Task InsertRangeAsync(List<TEntity> entitys)
        {
            await Table.AddRangeAsync(entitys);
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            await Task.FromResult(Table.Update(entity));
        }

       

        // <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
             await Task.FromResult(Table.Remove(entity));
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(TPrimaryKey id)
        {
            var model =await  Table.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (model != null)
                await Task.FromResult(Table.Remove(model));
        }
     

        #endregion




    }



}
