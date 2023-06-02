using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class FollowRepository : BaseEntityRepository<App.DAL.DTO.Follow, App.Domain.Follow, AppDbContext>, IFollowRepository
{
	public FollowRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Follow, App.Domain.Follow> mapper)
		: base(dbContext, mapper)
	{
	}

}