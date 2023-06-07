using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class DirectMessageRepository : BaseEntityRepository<DTO.DirectMessage, Domain.DirectMessage, AppDbContext>, IDirectMessageRepository
{
	public DirectMessageRepository(AppDbContext dbContext, IMapper<DTO.DirectMessage, Domain.DirectMessage> mapper)
		: base(dbContext, mapper)
	{
	}

}