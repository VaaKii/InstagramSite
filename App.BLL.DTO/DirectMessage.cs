
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

	public class DirectMessage: DomainEntityMetaId
	{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }
	public string? Message { get; set; }
	
	public Guid SenderId { get; set; }
	}

