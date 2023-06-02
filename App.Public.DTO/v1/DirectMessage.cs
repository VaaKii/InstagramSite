using Base.Domain;
using App.Public.DTO.v1.Identity;

namespace App.Public.DTO.v1;

public class DirectMessage : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public string? Message { get; set; }
	
    public Guid SenderId { get; set; }
}