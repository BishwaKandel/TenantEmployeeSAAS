using Application.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Services.Base
{
    public class RepositoryBase<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;


        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }


        public async Task DeleteAsync(T entity) => _dbSet.Remove(entity);
        public async Task DeleteRangeAsync(List<T> entities) => _dbSet.RemoveRange(entities);

        public IQueryable<T> FindAll(bool trackChanges)
           => !trackChanges ? _dbSet.AsNoTracking() : _dbSet;


        // trackChanges default is false: every existing caller either omits the flag or passes
        // false, and all of them are read paths (mutations go through Update*/Delete* which set
        // entity state explicitly), so reads skip the change tracker.
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool trackChanges = false)
        {
            IQueryable<T> query = _dbSet;
            if (!trackChanges) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (expression != null) query = query.Where(expression);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public IQueryable<T> FindByCondition(
               Expression<System.Func<T, bool>> expression, bool trackChanges) =>
               !trackChanges ?
               _dbSet.Where(expression).AsNoTracking()
               : _dbSet.Where(expression);

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool trackChanges = false)
        {
            IQueryable<T> query = _dbSet;
            if (!trackChanges) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (expression != null) query = query.Where(expression);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }

}
