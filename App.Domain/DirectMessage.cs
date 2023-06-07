
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.Domain;

	public class DirectMessage: DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
	{
	public string? Message { get; set; }
	
	public Guid ReceiverId { get; set; }
	public AppUser? Receiver { get; set; }
	
	public Guid AuthorId { get; set; }
	public AppUser? Author { get; set; }
	}

