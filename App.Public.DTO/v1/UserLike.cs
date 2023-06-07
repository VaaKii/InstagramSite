using Base.Domain;
using App.BLL.DTO.Identity;
using Base.Contracts.Domain;

namespace App.Public.DTO.v1;

public class UserLike: DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
	{

		public Guid LikedId { get; set; }
		public UserPost? Liked { get; set; }

		public Guid AuthorId { get; set; }
		public AppUser? Author { get; set; }
}

