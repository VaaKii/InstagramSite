using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserHashtagRepository : BaseEntityRepository<App.DAL.DTO.UserHashtag, App.Domain.UserHashtag, AppDbContext>, IUserHashtagRepository
{
	public UserHashtagRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.UserHashtag, App.Domain.UserHashtag> mapper)
		: base(dbContext, mapper)
	{
	}
	public UserHashtag AddWithUser(UserHashtag entity, Guid userId)
	{
		entity.AppUserId = userId;

		return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
	}
}