using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.DAL.DTO.Identity;

public class AppUser : BaseUser
{
    [StringLength(128)]
    public string? Firstname { get; set; }

    [StringLength(128)] public string? Lastname { get; set; }

    public ICollection<RefreshToken>? RefreshTokens { get; set; }

    public ICollection<UserPost>? UserPosts { get; set; }
    public ICollection<UserStory>? Stories { get; set; }
    public ICollection<DirectMessage>? DirectMessages { get; set; }

    
}
