using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class FollowRepository : BaseEntityRepository<DTO.Follow, Domain.Follow, AppDbContext>, IFollowRepository
{
	public FollowRepository(AppDbContext dbContext, IMapper<DTO.Follow, Domain.Follow> mapper)
		: base(dbContext, mapper)
	{
	}

}