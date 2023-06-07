using App.DAL.DTO.Identity;
using App.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
  public DbSet<DirectMessage> DirectMessages { get; set; } = default!;
  public DbSet<Follow> Follows { get; set; } = default!;
  public DbSet<Topic> Topics { get; set; } = default!;
  public DbSet<UserComment> UserComments { get; set; } = default!;
  public DbSet<UserHashtag> UserHashtags { get; set; } = default!;
  public DbSet<UserLike> UserLikes { get; set; } = default!;
  public DbSet<UserPost> UserPosts { get; set; } = default!;
  public DbSet<UserStory> UserStories { get; set; } = default!;
  public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
  
  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    //modelBuilder.Entity<Translation>().HasKey(k => new { k.Culture, k.LangStringId });

    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
    {
      relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }
  }

}