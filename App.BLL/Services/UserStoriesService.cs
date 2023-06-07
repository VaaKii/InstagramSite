using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserStoriesService : BaseEntityService<UserStory, App.DAL.DTO.UserStory, IUserStoriesRepository>,
	IUserStoriesService
{
	public UserStoriesService(IUserStoriesRepository repository, IMapper<UserStory, DAL.DTO.UserStory> mapper) : base(repository, mapper)
	{
	}

	public UserStory AddWithUser(UserStory entity, Guid userId)
	{
		return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
	}

}