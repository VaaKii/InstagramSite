using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserStoriesService : BaseEntityService<App.BLL.DTO.UserStories, App.DAL.DTO.UserStories, IUserStoriesRepository>,
	IUserStoriesService
{
	public UserStoriesService(IUserStoriesRepository repository, IMapper<UserStories, DAL.DTO.UserStories> mapper) : base(repository, mapper)
	{
	}

	public UserStories AddWithUser(UserStories entity, Guid userId)
	{
		return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
	}

}