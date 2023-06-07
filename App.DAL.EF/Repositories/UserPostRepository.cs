using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserPostRepository : BaseEntityRepository<UserPost, App.Domain.UserPost, AppDbContext>,
  IUserPostRepository
{
  public UserPostRepository(AppDbContext dbContext, IMapper<UserPost, App.Domain.UserPost> mapper)
    : base(dbContext, mapper)
  {
  }

  public override async Task<IEnumerable<UserPost>> GetAllAsync(Guid userId = default, bool noTracking = true)
  {
    var query = CreateQuery(userId, noTracking);

    return (await query.Include(s => s.UserComments).ToListAsync()).Select(x => Mapper.Map(x)!);
  }

  public override async Task<UserPost?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
  {
    var query = CreateQuery(userId, noTracking);
    query = query.Include(q => q.UserComments);
    return Mapper.Map(await query.FirstOrDefaultAsync(m => m.Id.Equals(id)));
  }

  public UserPost AddWithUser(UserPost entity, Guid userId)
  {
    entity.AuthorId = userId;

    return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
  }
}