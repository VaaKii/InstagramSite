using App.BLL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class UserPost : DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
{
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;

    public Guid TopicId { get; set; }
    public Topic? Topic { get; set; }
    public string? UrlPhoto { get; set; }
    
    public Guid AuthorId { get; set; }
    public AppUser? Author { get; set; }
    
    public ICollection<UserComment>? UserComments { get; set; }
    public ICollection<UserHashtag>? UserHashtags { get; set; }
    public ICollection<UserLike>? UserLikes { get; set; }
}