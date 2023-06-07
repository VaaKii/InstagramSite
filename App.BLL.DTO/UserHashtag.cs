using App.BLL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class UserHashtag : DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
{
	public Guid AuthorId { get; set; }
	public AppUser? Author { get; set; }
	
	public string HashtagText { get; set; } = default!;
	public ICollection<UserPost>? Posts { get; set; }
}

