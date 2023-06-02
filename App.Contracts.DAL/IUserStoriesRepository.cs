using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserStoriesRepository : IEntityRepository<App.DAL.DTO.UserStories>, IUserStoriesRepositoryCustom<UserStories>
{
}

public interface IUserStoriesRepositoryCustom<TEntity> {
	TEntity AddWithUser(TEntity entity, Guid userId);
}

