using Base.Domain;

namespace App.Domain;

public class UserHashtag : DomainEntityMetaId
{
	public string HashtagText { get; set; } = default!;
	public ICollection<UserPost>? Posts { get; set; }
}

