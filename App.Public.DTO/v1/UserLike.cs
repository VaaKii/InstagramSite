using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserLike : DomainEntityId
{
    public Guid UserPostId { get; set; }
    public UserPost? UserPost { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}