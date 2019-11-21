using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace core.seedwork
{
    public class Repository<TEntity, TEntityID> where TEntity : class, IEntidadeBase<TEntityID>
    {
        protected DbContext context { get; private set; }
        protected readonly DbSet<TEntity> query;
        public DbContext GetContext()
        {
            return context;
        }

        protected Repository(DbContext context, DbSet<TEntity> query)
        {
            this.context = context;
            this.query = query;
        }

        protected Repository(DbContext context)
        {
            this.context = context;
            query = context.Set<TEntity>();
        }

        #region Exists

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Any(predicate);
        }

        public bool Exist(TEntityID entidadeId)
        {
            return context.Set<TEntity>().Any(c => c.Id.Equals(entidadeId));
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<bool> ExistAsync(TEntityID entidadeId)
        {
            return await context.Set<TEntity>().AnyAsync(c => c.Id.Equals(entidadeId));
        }

        #endregion

        #region Get Item

        public TEntity Get(params object[] keyValues)
        {
            return query.Find(keyValues);
        }

        public virtual async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await query.FindAsync(keyValues);
        }

        #endregion

        #region Get All

        public IQueryable<TEntity> GetAll(bool asNoTracking = false)
        {
            if (asNoTracking)
            {
                return query.AsNoTracking();
            }

            return query;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            if (asNoTracking)
            {
                return query.AsNoTracking().Where(predicate).AsQueryable();
            }

            return query.Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var queryIncludes = query.Where(predicate).AsNoTracking().AsQueryable();

            if (includes != null)
            {
                queryIncludes = includes.Aggregate(queryIncludes, (current, include) => current.Include(include));
            }

            return query;
        }

        public IQueryable<TEntity> Filter(ISpecification<TEntity> specification)
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(query.AsQueryable(),
                    (current, include) => current.Include(include));

            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            foreach(var where in specification.Criterias)
            {
                secondaryResult = secondaryResult.Where(where);
            }

                return secondaryResult;

        }

        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            var param = Expression.Parameter(typeof(T), string.Empty); 
            var property = Expression.PropertyOrField(param, propertyName);
            var sort = Expression.Lambda(property, param);
            var call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public (IQueryable<TEntity> items, int pageCount, int total) Paginate(ISpecificationPaginate<TEntity> specification)
        {
            
            var result = Filter(specification);


            var rowCount = result.Count();
            var pageCount = (double)rowCount / specification.PageSize;
            var totalCount = (int)Math.Ceiling(pageCount);

            var page = specification.Page < 1 ? 1 : specification.Page;

            var skip = (page - 1) * specification.PageSize;
            if (!String.IsNullOrEmpty(specification.Order))
            {
                result = OrderingHelper(result, specification.Order, false, false);
                result = result.Skip(skip).Take(specification.PageSize);
                return (result, totalCount, rowCount);
            }
            else
            {
                result = result.Skip(skip).Take(specification.PageSize);
                return (result, totalCount, rowCount);
            }

        }

        #endregion

        #region Create/Update/Delete

        public TEntity Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            query.Add(entity);

            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await query.AddAsync(entity);

            return entity;
        }

        public async Task CreateAsync(ICollection<TEntity> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await query.AddRangeAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            query.Update(entity);

            return entity;
        }


        public TEntity Delete(TEntityID id)
        {
            var entity = query.Find(id);
            query.Remove(entity);

            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            query.Remove(entity);

            return entity;
        }

        public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var entities = query.Where(predicate);

            query.RemoveRange(entities);
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Inicia Transação

        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        #endregion

        public async Task<bool> CommitAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool Commit()
        {
            return context.SaveChanges() > 0;
        }
    }
}
