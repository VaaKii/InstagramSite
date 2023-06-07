using App.BLL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Follow : DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
{
	public Guid AuthorId { get; set; }
	public AppUser? Author { get; set; }
	
	public Guid FolloweeId { get; set; }
	public AppUser? Followee { get; set; }
}

