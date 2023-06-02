using App.Domain.Identity;
using Base.Domain;


namespace App.Domain;

	public class UserStories: DomainEntityMetaId
	{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }

	public string? UrlPhoto { get; set; }
	}

