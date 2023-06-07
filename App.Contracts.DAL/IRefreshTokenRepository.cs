using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRefreshTokenRepository: IEntityRepository<App.DAL.DTO.Identity.RefreshToken> { }
    
public interface IRefreshTokenRepositoryCustom<TEntity> 
{

}