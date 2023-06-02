using App.BLL.DTO.Identity;
using Base.Domain;
namespace App.BLL.DTO;

	public class UserStories: DomainEntityMetaId
	{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }

	public string? UrlPhoto { get; set; }
	}

