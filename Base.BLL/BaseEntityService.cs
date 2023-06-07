using Base.Contracts.Base;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Base.BLL;

public class
    BaseEntityService<TBllEntity, TDalEntity, TRepository> :
        BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>, IEntityService<TBllEntity>
    where TDalEntity : class, IDomainEntityId
    where TBllEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    protected TRepository Repository;
    protected IMapper<TBllEntity, TDalEntity> Mapper;

    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity, TKey? userId = default)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!, userId))!;
    }

    public TBllEntity Remove(TBllEntity entity, TKey? userId = default)
    {
        return Mapper.Map(Repository.Remove(Mapper.Map(entity)!, userId))!;
    }

    public TBllEntity Remove(TKey id, TKey? userId = default)
    {
        return Mapper.Map(Repository.Remove(id, userId))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(Repository.FirstOrDefault(id, userId, noTracking))!;
    }

    public IEnumerable<TBllEntity> GetAll(TKey? userId = default, bool noTracking = true)
    {
        return Repository.GetAll(userId, noTracking).Select(x => Mapper.Map(x)!);
    }

    public bool Exists(TKey id, TKey? userId = default)
    {
        return Repository.Exists(id, userId);
    }
    
    // TODO IF NEEDED
    public EntityEntry<TBllEntity> Entry(TBllEntity entity, TKey? userId = default)
    {
        throw new NotImplementedException();
    }


    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!);
    }

    public Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        return Repository.ExistsAsync(id, userId);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id, TKey? userId = default)
    {
        return Mapper.Map(await Repository.RemoveAsync(id, userId))!;
    }
}
