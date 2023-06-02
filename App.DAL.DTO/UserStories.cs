using App.DAL.DTO.Identity;
using Base.Domain;
namespace App.DAL.DTO;

	public class UserStories: DomainEntityMetaId
	{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }

	public string? UrlPhoto { get; set; }
	}

