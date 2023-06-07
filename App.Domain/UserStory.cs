using App.Domain.Identity;
using Base.Domain;


namespace App.Domain;

	public class UserStory: DomainEntityMetaId
	{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }

	public string? UrlPhoto { get; set; }
	}

