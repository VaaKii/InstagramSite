using App.Contracts.DAL;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AppUserRepository: BaseEntityRepository<AppUser, App.Domain.Identity.AppUser, AppDbContext>, IAppUserRepository
{
    public AppUserRepository(AppDbContext dbContext, IMapper<AppUser, Domain.Identity.AppUser> mapper) 
        : base(dbContext, mapper)
    {
    }
    
}