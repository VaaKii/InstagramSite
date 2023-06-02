using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserLikeService : IEntityService<App.BLL.DTO.UserLike>, IUserLikeRepositoryCustom<App.BLL.DTO.UserLike>
{
    
}