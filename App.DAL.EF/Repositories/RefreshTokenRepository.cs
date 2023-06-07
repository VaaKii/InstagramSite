using App.Contracts.DAL;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RefreshTokenRepository: BaseEntityRepository<RefreshToken, App.Domain.Identity.RefreshToken, AppDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext dbContext, IMapper<RefreshToken, Domain.Identity.RefreshToken> mapper) 
        : base(dbContext, mapper)
    {
    }
    
}