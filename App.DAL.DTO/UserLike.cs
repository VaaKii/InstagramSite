using Base.Domain;
using App.DAL.DTO.Identity;

namespace App.DAL.DTO;

public class UserLike: DomainEntityMetaId
	{

		public Guid UserPostId { get; set; }
		public UserPost? UserPost { get; set; }

		public Guid AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
}

