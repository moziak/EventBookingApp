using System;
using System.Linq.Expressions;
using EventBooking.Domain.Common.Helpers;

namespace EventBooking.Application.Common.Interfaces.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<PaginatedList<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}


