using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork{
    private readonly IMapper _mapper;
    public AppUOW(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
    {
        _mapper = mapper;
    }

    private IAppUserRepository? _appUsers;

    public IAppUserRepository AppUsers =>
        _appUsers ??= new AppUserRepository(uowDbContext, new AppUserMapper(_mapper));


    private IDirectMessageRepository? _directMessages;
    public virtual IDirectMessageRepository DirectMessages =>
        _directMessages ??= new DirectMessageRepository(uowDbContext, new DirectMessageMapper(_mapper));

    private IFollowRepository? _follows;
    public virtual IFollowRepository Follows =>
        _follows ??= new FollowRepository(uowDbContext, new FollowMapper(_mapper));
    
    private ITopicRepository? _topics;
    public virtual ITopicRepository Topics =>
        _topics ??= new TopicRepository(uowDbContext, new TopicMapper(_mapper));

    private IUserHashtagRepository? _userHashtags;
    public virtual IUserHashtagRepository UserHashtags =>
		_userHashtags ??= new UserHashtagRepository(uowDbContext, new UserHashtagMappper(_mapper));

    private IUserCommentRepository? _userComments;
    public virtual IUserCommentRepository UserComments =>
        _userComments ??= new UserCommentRepository(uowDbContext, new UserCommentMapper(_mapper));

    private IUserPostRepository? _userPosts;
    public virtual IUserPostRepository UserPosts =>
        _userPosts ??= new UserPostRepository(uowDbContext, new UserPostMapper(_mapper));

    private IUserStoriesRepository? _userStories;

    public virtual IUserStoriesRepository UserStories =>
        _userStories ??= new UserStoriesRepository(uowDbContext, new UserStoriesMapper(_mapper));

    private IUserLikeRepository? _userLikes;

    public virtual IUserLikeRepository UserLikes =>
        _userLikes ??= new UserLikeRepository(uowDbContext,new UserLikeMapper(_mapper));
    
    private IRefreshTokenRepository? _refreshTokens;

    public virtual IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(uowDbContext,new RefreshTokenMapper(_mapper));
}