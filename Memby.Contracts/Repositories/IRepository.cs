using Memby.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Memby.Contracts.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        TEntity Get(object id);

        TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        List<TEntity> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity Insert(TEntity entity, bool autosave = true);

        TEntity Save(TEntity entity);

        void Update(TEntity entity, bool autosave = true);

        void Delete(TEntity entity, bool autosave = true);

        int SaveChanges();
    }
}
