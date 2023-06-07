using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserLikeRepository : BaseEntityRepository<UserLike, App.Domain.UserLike, AppDbContext>,
  IUserLikeRepository
{
  public UserLikeRepository(AppDbContext dbContext, IMapper<UserLike, App.Domain.UserLike> mapper)
    : base(dbContext, mapper)
  {
  }

  public UserLike AddWithUser(UserLike entity, Guid userId)
  {
    entity.AuthorId = userId;

    return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
  }
}