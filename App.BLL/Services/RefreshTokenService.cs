using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class RefreshTokenService: BaseEntityService<DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken, IRefreshTokenRepository>,
    IRefreshTokenService
{
    public RefreshTokenService(IRefreshTokenRepository repository, IMapper<DTO.Identity.RefreshToken, DAL.DTO.Identity.RefreshToken> mapper) : base(repository, mapper)
    {
    }
}