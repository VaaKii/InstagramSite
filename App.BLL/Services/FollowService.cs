using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class FollowService : BaseEntityService<Follow, App.DAL.DTO.Follow, IFollowRepository>,
    IFollowService
{
    public FollowService(IFollowRepository repository, IMapper<Follow, DAL.DTO.Follow> mapper) : base(repository, mapper)
    {
    }
}