using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Follow : DomainEntityMetaId
{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }

}

