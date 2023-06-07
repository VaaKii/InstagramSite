using App.DAL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class UserLike: DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
	{

		public Guid LikedId { get; set; }
		public UserPost? Liked { get; set; }

		public Guid AuthorId { get; set; }
		public AppUser? Author { get; set; }
}

