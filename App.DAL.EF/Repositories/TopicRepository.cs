using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TopicRepository: BaseEntityRepository<Topic, App.Domain.Topic, AppDbContext>, ITopicRepository
{
    public TopicRepository(AppDbContext dbContext, IMapper<Topic,App.Domain.Topic> mapper) 
        : base(dbContext, mapper)
    {
    }

    public override Topic Update(Topic entity, Guid userId = default)
    {
        var realEntity = RepoDbSet.FindAsync(entity.Id).Result;

        realEntity!.Name.SetTranslation(entity.Name);
        realEntity.Description.SetTranslation(entity.Description);
        
        return Mapper.Map(RepoDbSet.Update(realEntity).Entity)!;
    }

    public override async Task<IEnumerable<Topic>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);

        return (await query.Include(s => s.UserPosts).ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    public override async Task<Topic?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        var query = CreateQuery(userId, noTracking);
        query = query.Include(q => q.UserPosts);
        return  Mapper.Map(await query.FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
}