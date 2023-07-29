using CQRS_JWTApp.API.Core.Application.Interfaces;
using CQRS_JWTApp.API.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRS_JWTApp.API.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly CqrsJwtContext _cqrsJwtContext;

        public Repository(CqrsJwtContext cqrsJwtContext)
        {
            _cqrsJwtContext = cqrsJwtContext;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _cqrsJwtContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _cqrsJwtContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _cqrsJwtContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task CreateAsync(T entity)
        {
            await _cqrsJwtContext.Set<T>().AddAsync(entity);
            await _cqrsJwtContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _cqrsJwtContext.Set<T>().Update(entity);
            await _cqrsJwtContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            _cqrsJwtContext.Set<T>().Remove(entity);
            await _cqrsJwtContext.SaveChangesAsync();
        }

    }
}
