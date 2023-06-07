using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserCommentRepository: BaseEntityRepository<UserComment, App.Domain.UserComment, AppDbContext>, 
    IUserCommentRepository
{
    public UserCommentRepository(AppDbContext dbContext, IMapper<UserComment,App.Domain.UserComment> mapper) 
        : base(dbContext, mapper)
    {
    }

    public UserComment AddWithUser(UserComment entity, Guid userId)
    {
        entity.AuthorId = userId;
        
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }
}