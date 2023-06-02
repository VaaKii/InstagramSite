using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserHashtagRepository : IEntityRepository<App.DAL.DTO.UserHashtag>, IUserHashtagRepositoryCustom<UserHashtag>
{
}

public interface IUserHashtagRepositoryCustom<TEntity> {
	TEntity AddWithUser(TEntity entity, Guid userId);
}

