using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace core.seedwork
{
    public interface IRepository<TEntity, TEntityID>
    {
        DbContext GetContext();

        #region Exists

        bool Exist(Expression<Func<TEntity, bool>> predicate);

         bool Exist(TEntityID entidadeId);

        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistAsync(TEntityID entidadeId);

        #endregion

        #region Get Item

        TEntity Get(params object[] keyValues);
        
        Task<TEntity> GetAsync(params object[] keyValues);

        #endregion

        #region Get All

        IQueryable<TEntity> GetAll(bool asNoTracking = false);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Filter(ISpecification<TEntity> specification);

        (IQueryable<TEntity> items, int pageCount, int total) Paginate(ISpecificationPaginate<TEntity> specification);

        #endregion

        #region Create/Update/Delete

        TEntity Create(TEntity entity);

        Task<TEntity> CreateAsync(TEntity entity);

        Task CreateAsync(ICollection<TEntity> entity);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        TEntity Delete(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);

        Task DeleteRangeAsync(TEntity[] entity);

        void Dispose();

        #endregion

        #region Inicia Transação

        IDbContextTransaction IniciaTransacao();

        #endregion
    }
}
