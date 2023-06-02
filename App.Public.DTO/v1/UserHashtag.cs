using System.ComponentModel.DataAnnotations;
using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserHashtag : DomainEntityId
{
    public string HashtagText { get; set; } = default!;
    public ICollection<UserPost>? Posts { get; set; }
}