using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        // trackChanges defaults to false: reads skip the EF change tracker; mutations use the
        // explicit Update*/Delete* methods which attach and set entity state themselves.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      List<Expression<Func<T, object>>> includes = null,
                                      bool trackChanges = false);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool trackChanges = false);

        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(
               Expression<System.Func<T, bool>> expression, bool trackChanges);
    }
}
