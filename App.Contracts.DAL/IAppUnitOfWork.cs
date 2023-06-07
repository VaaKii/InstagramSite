using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IDirectMessageRepository DirectMessages { get; }
    IFollowRepository Follows { get; }
    IUserHashtagRepository UserHashtags { get; }
    IUserLikeRepository UserLikes { get; }
    IUserStoriesRepository UserStories { get; }
    IAppUserRepository AppUsers { get; }
    ITopicRepository Topics { get; }
    IUserCommentRepository UserComments { get; }
    IUserPostRepository UserPosts { get; }
    IRefreshTokenRepository RefreshTokens { get; }
}