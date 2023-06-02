using System.ComponentModel.DataAnnotations;
using Base.Domain;
using App.Public.DTO.v1.Identity;

namespace App.Public.DTO.v1;

public class Follow : DomainEntityId // TODO UPDATE DB!
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}