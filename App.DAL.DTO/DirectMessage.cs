
using App.DAL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

	public class DirectMessage: DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
	{
	public Guid AuthorId { get; set; }
	public AppUser? Author { get; set; }
	public string? Message { get; set; }
	
	public Guid ReceiverId { get; set; }
	public AppUser? Receiver { get; set; }
	}

