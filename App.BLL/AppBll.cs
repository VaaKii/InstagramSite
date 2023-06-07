using App.BLL.Mappers;
using App.BLL.Mappers.Identity;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBll: BaseBll<IAppUnitOfWork>, IAppBll
{
    private readonly IMapper _mapper;

    protected IAppUnitOfWork UnitOfWork;
    public AppBll(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    // private IAppUserService? _users;
    // public IAppUserService Users =>
    // _users ?? = new AppUserService(UnitOfWork)

    private IAppUserService? _appUsers;
    public IAppUserService AppUsers =>
        _appUsers ??= new AppUserService(UnitOfWork.AppUsers, new AppUserMapper(_mapper));

    private IDirectMessageService? _directMessages;
    public IDirectMessageService DirectMessages =>
        _directMessages ??= new DirectMessageService(UnitOfWork.DirectMessages, new DirectMessageMapper(_mapper));

    private IFollowService? _follows;
    public IFollowService Follows =>
        _follows ??= new FollowService(UnitOfWork.Follows, new FollowMapper(_mapper));

    private ITopicService? _topics;
    public ITopicService Topics =>
        _topics ??= new TopicService(UnitOfWork.Topics, new TopicMapper(_mapper));

    private IUserHashtagService? _userHashtag;
    public IUserHashtagService UserHashtags =>
		_userHashtag ??= new UserHashtagService(UnitOfWork.UserHashtags, new UserHashtagMapper(_mapper));

    private IUserCommentService? _userComments;
    public IUserCommentService UserComments =>
        _userComments ??= new UserCommentService(UnitOfWork.UserComments, new UserCommentMapper(_mapper));

    private IUserPostService? _userPosts;
    public IUserPostService UserPosts =>
        _userPosts ??= new UserPostService(UnitOfWork.UserPosts, new UserPostMapper(_mapper));

    private IUserLikeService? _userLikes;
    public IUserLikeService UserLikes =>
        _userLikes ??= new UserLikeService(UnitOfWork.UserLikes, new UserLikeMapper(_mapper));

    private IUserStoriesService? _userStories;
    public IUserStoriesService UserStories =>
        _userStories ??= new UserStoriesService(UnitOfWork.UserStories, new UserStoriesMapper(_mapper));
    
    private IRefreshTokenService? _refreshTokens;
    public IRefreshTokenService RefreshTokens =>
        _refreshTokens ??= new RefreshTokenService(UnitOfWork.RefreshTokens, new RefreshTokenMapper(_mapper));
}