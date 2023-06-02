using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserLikeRepository : IEntityRepository<App.DAL.DTO.UserLike>, IUserLikeRepositoryCustom<UserLike>
{
}

public interface IUserLikeRepositoryCustom<TEntity> {
	TEntity AddWithUser(TEntity entity, Guid userId);
}

