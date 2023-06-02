using App.Domain;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AppUser> AppUsers { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<UserStories> UserStories { get; set; } = default!;
    public DbSet<DirectMessage> DirectMessages { get; set; } = default!;
    public DbSet<Follow> Follows { get; set; } = default!;


    
    // For Posts feature

    public DbSet<Topic> Topics { get; set; } = default!;

    public DbSet<UserPost> UserPosts { get; set; } = default!;

    public DbSet<UserComment> UserComments { get; set; } = default!;
    public DbSet<UserLike> UserLikes { get; set; } = default!;
    public DbSet<UserHashtag> UserHashtags { get; set; } = default!;

    
    // For todos

    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }


    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            ///
            builder
                .Entity<App.Domain.Topic>()
                .Property(e => e.Name)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<App.Domain.Topic>()
                .Property(e => e.Description)
                .HasConversion(v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
        }
    }
    
    
    private static string SerialiseLangStr(LangStr lStr) => System.Text.Json.JsonSerializer.Serialize(lStr);

    private static LangStr DeserializeLangStr(string jsonStr) =>
        System.Text.Json.JsonSerializer.Deserialize<LangStr>(jsonStr) ?? new LangStr();
}