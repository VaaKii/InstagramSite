using App.BLL.DTO.Identity;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class UserComment : DomainEntityMetaId, IDomainAppUser<Guid, AppUser>
{
    public string CommentText { get; set; } = default!;

    public Guid UserPostId { get; set; }
    public UserPost? UserPost { get; set; }
    
    public Guid AuthorId { get; set; }
    public AppUser? Author { get; set; }
}