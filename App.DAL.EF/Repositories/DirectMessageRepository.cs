using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class DirectMessageRepository : BaseEntityRepository<App.DAL.DTO.DirectMessage, App.Domain.DirectMessage, AppDbContext>, IDirectMessageRepository
{
	public DirectMessageRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.DirectMessage, App.Domain.DirectMessage> mapper)
		: base(dbContext, mapper)
	{
	}

}