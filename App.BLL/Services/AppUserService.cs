using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class AppUserService: BaseEntityService<DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser, IAppUserRepository>,
    IAppUserService
{
    public AppUserService(IAppUserRepository repository, IMapper<DTO.Identity.AppUser, DAL.DTO.Identity.AppUser> mapper) : base(repository, mapper)
    {
    }
}