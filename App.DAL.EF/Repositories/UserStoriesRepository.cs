using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserStoriesRepository : BaseEntityRepository<App.DAL.DTO.UserStories, App.Domain.UserStories, AppDbContext>, IUserStoriesRepository
{
	public UserStoriesRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserStories, App.Domain.UserStories> mapper)
		: base(dbContext, mapper)
	{
	}
	public UserStories AddWithUser(UserStories entity, Guid userId)
	{
		entity.AppUserId = userId;

		return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
	}
}