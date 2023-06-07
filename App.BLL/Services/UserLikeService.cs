using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserLikeService : BaseEntityService<UserLike, App.DAL.DTO.UserLike, IUserLikeRepository>,
    IUserLikeService
{
    public UserLikeService(IUserLikeRepository repository, IMapper<UserLike, DAL.DTO.UserLike> mapper) : base(repository, mapper)
    {
    }

    public UserLike AddWithUser(UserLike entity, Guid userId)
    {
        return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
    }
    
}