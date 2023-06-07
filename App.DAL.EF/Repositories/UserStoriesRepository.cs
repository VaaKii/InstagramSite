using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserStoriesRepository : BaseEntityRepository<UserStory, App.Domain.UserStory, AppDbContext>,
  IUserStoriesRepository
{
  public UserStoriesRepository(AppDbContext dbContext, IMapper<UserStory, App.Domain.UserStory> mapper)
    : base(dbContext, mapper)
  {
  }

  public UserStory AddWithUser(UserStory entity, Guid userId)
  {
    entity.AuthorId = userId;

    return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
  }
}