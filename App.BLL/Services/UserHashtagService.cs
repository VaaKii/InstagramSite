using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UserHashtagService : BaseEntityService<App.BLL.DTO.UserHashtag, App.DAL.DTO.UserHashtag, IUserHashtagRepository>,
    IUserHashtagService
{
    public UserHashtagService(IUserHashtagRepository repository, IMapper<UserHashtag, DAL.DTO.UserHashtag> mapper) : base(repository, mapper)
    {
    }

    public UserHashtag AddWithUser(UserHashtag entity, Guid userId)
    {
        return Mapper.Map(Repository.AddWithUser(Mapper.Map(entity)!, userId))!;
    }
}