using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserStoriesService : IEntityService<App.BLL.DTO.UserStories>, IUserStoriesRepositoryCustom<App.BLL.DTO.UserStories>

{

}