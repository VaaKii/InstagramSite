using System.Security.Authentication;
using Base.Contracts.Base;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Base.DAL.EF;

public class
  BaseEntityRepository<TDalEntity, TDomainEntity, TDbContext> : BaseEntityRepository<TDalEntity, TDomainEntity, Guid,
    TDbContext>
  where TDalEntity : class, IDomainEntityId<Guid>
  where TDomainEntity : class, IDomainEntityId<Guid>
  where TDbContext : DbContext
{
  public BaseEntityRepository(
    TDbContext dbContext,
    IMapper<TDalEntity,
      TDomainEntity> mapper) : base(dbContext, mapper)
  {
  }
}

public class BaseEntityRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : IEntityRepository<TDalEntity, TKey>
  where TDalEntity : class, IDomainEntityId<TKey>
  where TDomainEntity : class, IDomainEntityId<TKey>
  where TKey : IEquatable<TKey>
  where TDbContext : DbContext
{
  protected readonly TDbContext RepoDbContext;
  protected readonly DbSet<TDomainEntity> RepoDbSet;
  protected readonly IMapper<TDalEntity, TDomainEntity> Mapper;

  public BaseEntityRepository(
    TDbContext dbContext,
    IMapper<TDalEntity, TDomainEntity> mapper
  )
  {
    RepoDbContext = dbContext;
    RepoDbSet = dbContext.Set<TDomainEntity>();
    Mapper = mapper;
  }

  protected virtual IQueryable<TDomainEntity> CreateQuery(TKey? userId = default, bool noTracking = true)
  {
    var query = RepoDbSet.AsQueryable();
    if (userId != null && !userId.Equals(default) &&
        typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
    {
      query = query.Where(e => ((IDomainAppUserId<TKey>)e).AuthorId.Equals(userId));
    }

    return noTracking ? query.AsNoTracking() : query;
  }

  public virtual TDalEntity Add(TDalEntity entity)
  {
    return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
  }

  public virtual TDalEntity Update(TDalEntity entity, TKey? userId = default)
  {
    if (userId != null && !userId.Equals(default) &&
        typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
        !((IDomainAppUserId<TKey>)entity).AuthorId.Equals(userId))
    {
      throw new AuthenticationException(
        $"Bad user id inside entity {typeof(TDalEntity).Name} to be deleted.");
    }

    return Mapper.Map(
      RepoDbSet.Update(
          Mapper.Map(entity)!)
        .Entity)!;
  }

  public virtual TDalEntity Remove(TDalEntity entity, TKey? userId = default)
  {
    if (userId != null && !userId.Equals(default) &&
        typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
        !((IDomainAppUserId<TKey>)entity).AuthorId.Equals(userId))
    {
      throw new AuthenticationException(
        $"Bad user id inside entity {typeof(TDalEntity).Name} to be deleted.");
    }

    return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;
  }

  public virtual TDalEntity Remove(TKey id, TKey? userId = default)
  {
    var entity = FirstOrDefault(id);
    if (entity == null)
    {
      throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
    }

    return Remove(entity, userId);
  }

  public virtual TDalEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true)
  {
    return Mapper.Map(
      CreateQuery(userId, noTracking)
        .FirstOrDefault(a => a.Id.Equals(id))
    );
  }

  public virtual IEnumerable<TDalEntity> GetAll(TKey? userId = default, bool noTracking = true)
  {
    return CreateQuery(userId, noTracking).ToList()
      .ToList()
      .Select(x => Mapper.Map(x)!);
  }

  public virtual bool Exists(TKey id, TKey? userId = default)
  {
    if (userId == null || userId.Equals(default))
      return RepoDbSet.Any(e => e.Id.Equals(id));

    if (!typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
      throw new AuthenticationException(
        $"Entity {typeof(TDomainEntity).Name} does not implement required interface: {typeof(IDomainAppUserId<TKey>).Name} for AppUserId check");

    return RepoDbSet
      .Any(e => e.Id.Equals(id) && ((IDomainAppUserId<TKey>)e).AuthorId.Equals(userId));
  }

  public virtual EntityEntry<TDalEntity> Entry(TDalEntity entity, TKey? userId = default)
  {
    if (userId != null && !userId.Equals(default) &&
        typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
        !((IDomainAppUserId<TKey>)entity).AuthorId.Equals(userId))
    {
      throw new AuthenticationException(
        $"Bad user id inside entity {typeof(TDalEntity).Name} to be deleted.");
    }

    return RepoDbContext.Entry(entity);
  }

  public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
  {
    return Mapper.Map(
      await CreateQuery(userId, noTracking)
        .FirstOrDefaultAsync(a => a.Id.Equals(id))
    );
  }

  public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
  {
    return (
        await CreateQuery(userId, noTracking)
          .ToListAsync())
      .Select(x => Mapper.Map(x)!);
  }

  public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
  {
    if (userId == null || userId.Equals(default))
      return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));

    if (!typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
      throw new AuthenticationException(
        $"Entity {typeof(TDomainEntity).Name} does not implement required interface: {typeof(IDomainAppUserId<TKey>).Name} for AppUserId check");

    return await RepoDbSet
      .AnyAsync(e => e.Id.Equals(id) && ((IDomainAppUserId<TKey>)e).AuthorId.Equals(userId));
  }

  public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey? userId = default)
  {
    var entity = await FirstOrDefaultAsync(id, userId);
    if (entity == null)
      throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} not found.");
    return Remove(entity!, userId);
  }
}