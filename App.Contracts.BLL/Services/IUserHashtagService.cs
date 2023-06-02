using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserHashtagService : IEntityService<App.BLL.DTO.UserHashtag>, IUserHashtagRepositoryCustom<App.BLL.DTO.UserHashtag>
{
    
}