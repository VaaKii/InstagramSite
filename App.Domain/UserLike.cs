using Base.Domain;
using App.Domain.Identity;

namespace App.Domain;

public class UserLike: DomainEntityMetaId
	{

		public Guid UserPostId { get; set; }
		public UserPost? UserPost { get; set; }

		public Guid AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
}

