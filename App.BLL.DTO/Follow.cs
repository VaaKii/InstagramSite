using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Follow : DomainEntityMetaId
{
	public Guid AppUserId { get; set; }
	public AppUser? AppUser { get; set; }
}

