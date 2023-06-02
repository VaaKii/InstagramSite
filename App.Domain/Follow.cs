using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Follow: DomainEntityMetaId
{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }
}

