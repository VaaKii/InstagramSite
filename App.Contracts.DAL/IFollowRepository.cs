using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IFollowRepository : IEntityRepository<App.DAL.DTO.Follow>, IFollowRepositoryCustom<Follow>
{
}

public interface IFollowRepositoryCustom<TEntity> { }

