using System.Collections;
using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;
namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    [StringLength(128)]
    public string? Firstname { get; set; }

    [StringLength(128)] public string? Lastname { get; set; }

    public ICollection<RefreshToken>? RefreshTokens { get; set; }

    public ICollection<UserPost>? UserPosts { get; set; }
    public ICollection<UserStories>? Stories { get; set; }
    public ICollection<DirectMessage>? DirectMessages { get; set; }
    
}
