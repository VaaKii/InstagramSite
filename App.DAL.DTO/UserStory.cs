using App.DAL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

	public class UserStory: DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
	{
	public Guid AuthorId { get; set; }
	public AppUser? Author { get; set; }

	public string? UrlPhoto { get; set; }
	}

