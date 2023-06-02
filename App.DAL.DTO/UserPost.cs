using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class UserPost : DomainEntityMetaId
{
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;

    public Guid TopicId { get; set; }
    public Topic? Topic { get; set; }
    public string? UrlPhoto { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<UserComment>? UserComments { get; set; }
    public ICollection<UserHashtag>? UserHashtags { get; set; }
    public ICollection<UserLike>? UserLikes { get; set; }
}