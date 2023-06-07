using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUOW<TDbContext>: IUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext uowDbContext;

    public BaseUOW(TDbContext dbContext)
    {
        uowDbContext = dbContext;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await uowDbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return uowDbContext.SaveChanges();
    }
}