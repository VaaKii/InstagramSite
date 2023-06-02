
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

	public class DirectMessage: DomainEntityMetaId
	{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }
	public string? Message { get; set; }
	
	public Guid SenderId { get; set; }
	}

