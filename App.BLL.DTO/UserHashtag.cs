using App.BLL.DTO.Identity;
using Base.Domain;
using Base.Domain.Identity;

namespace App.BLL.DTO;

public class UserHashtag : DomainEntityMetaId
{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }
	public string HashtagText { get; set; } = default!;
	public ICollection<UserPost>? Posts { get; set; }
}

