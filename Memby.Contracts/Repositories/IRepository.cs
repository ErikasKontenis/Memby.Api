using Memby.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Memby.Contracts.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        TEntity Get(int id);

        Task<TEntity> GetAsync(int id);

        TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        List<TEntity> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity Insert(TEntity entity, bool autosave = true);

        Task<TEntity> InsertAsync(TEntity entity, bool autosave = true);

        TEntity Save(TEntity entity);

        Task<TEntity> SaveAsync(TEntity entity);

        void Update(TEntity entity, bool autosave = true);

        Task UpdateAsync(TEntity entity, bool autosave = true);

        void Delete(TEntity entity, bool autosave = true);

        Task DeleteAsync(TEntity entity, bool autosave = true);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
