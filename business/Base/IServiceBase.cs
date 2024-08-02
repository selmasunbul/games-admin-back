using System;
using System.Threading.Tasks;
using data_access.BaseEntity;

namespace business.Base
{
    public interface IServiceBase<TEntity>
        where TEntity : class, IEntityBase, new()
    {
        Task<TEntity?> AddAsync(TEntity entity);
    }
}