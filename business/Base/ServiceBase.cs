using System;
using System.Threading.Tasks;
using data_access.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace business.Base
{
    public class ServiceBase<TEntity, TContext> : IServiceBase<TEntity>
        where TEntity : class, IEntityBase, new()
        where TContext : DbContext
    {
        private readonly TContext _context;

        public ServiceBase(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return entity;

            return null;
        }

    }
}
