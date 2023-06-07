using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBll : IBLL
{
    IAppUserService AppUsers { get; }
    IDirectMessageService DirectMessages { get; }
    IFollowService Follows { get; }
    ITopicService Topics { get; }
    IUserHashtagService UserHashtags { get; }
    IUserCommentService UserComments { get; }
    IUserPostService UserPosts { get; }
    IUserLikeService UserLikes { get; }
    IUserStoriesService UserStories { get; }
    IRefreshTokenService RefreshTokens { get; }
    
}