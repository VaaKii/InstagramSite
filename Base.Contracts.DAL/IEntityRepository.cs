using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
where TEntity: class, IDomainEntityId
{
}

public interface IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity, TKey? userId = default);
    TEntity Remove(TEntity entity, TKey? userId = default);
    TEntity Remove(TKey id, TKey? userId = default);
    TEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey? userId = default, bool noTracking = true);
    bool Exists(TKey id, TKey? userId = default);
    EntityEntry<TEntity> Entry(TEntity entity, TKey? userId = default);
    
    //async
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id, TKey? userId = default);
    Task<TEntity> RemoveAsync(TKey id, TKey? userId = default);
}