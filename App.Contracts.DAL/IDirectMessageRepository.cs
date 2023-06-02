using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IDirectMessageRepository: IEntityRepository<App.DAL.DTO.DirectMessage>, IDirectMessageRepositoryCustom<DirectMessage>
{
}

public interface IDirectMessageRepositoryCustom<TEntity> { }

