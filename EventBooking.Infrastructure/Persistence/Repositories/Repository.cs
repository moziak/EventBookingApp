using System;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EventBooking.Domain.Common.Helpers;
using EventBooking.Domain.Common.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventBooking.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EventBookingDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(EventBookingDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<PaginatedList<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            IQueryable<T> query = _dbSet.Where(predicate).AsNoTracking();
            var result = new PaginatedList<T>();
            result.TotalCount = query.Count();
            var skip = (pageIndex - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;


            return result;

        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

