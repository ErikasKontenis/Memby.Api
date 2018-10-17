using Memby.Contracts.Repositories;
using Memby.Data.DbContexts;
using Memby.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Memby.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly char[] _coma = { ',' };
        private readonly DbSet<TEntity> _dbSet;
        private protected readonly MembyDbContext _dbContext;

        public Repository(MembyDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            return _dbSet.FirstOrDefault(o => o.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.Id == id);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            query = includeProperties
                .Split(_coma, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.FirstOrDefault(filter);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            query = includeProperties
                .Split(_coma, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(filter);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties
                .Split(_coma, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : query;
        }

        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return Query(filter, orderBy, includeProperties).ToList();
        }

        public virtual async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await Query(filter, orderBy, includeProperties).ToListAsync();
        }

        public virtual TEntity Insert(TEntity entity, bool autosave = true)
        {
            entity = _dbSet.Add(entity).Entity;

            if (autosave)
            {
                SaveChanges();
            }

            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autosave = true)
        {
            entity = _dbSet.Add(entity).Entity;

            if (autosave)
            {
                await SaveChangesAsync();
            }

            return entity;
        }

        public virtual TEntity Save(TEntity entity)
        {
            entity = Insert(entity);

            return entity;
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);

            return entity;
        }

        public virtual void Update(TEntity entity, bool autosave = true)
        {
            if (_dbSet.Local.All(e => e != entity))
            {
                _dbSet.Attach(entity);
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            if (autosave)
            {
                SaveChanges();
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, bool autosave = true)
        {
            if (_dbSet.Local.All(e => e != entity))
            {
                _dbSet.Attach(entity);
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            if (autosave)
            {
                await SaveChangesAsync();
            }
        }

        public virtual void Delete(TEntity entity, bool autosave = true)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            if (autosave)
            {
                SaveChanges();
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autosave = true)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            if (autosave)
            {
                await SaveChangesAsync();
            }
        }

        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
