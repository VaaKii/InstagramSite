using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork{
    private readonly AutoMapper.IMapper _mapper;
    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IAppUserRepository? _appUsers;

    public IAppUserRepository AppUsers =>
        _appUsers ??= new AppUserRepository(UOWDbContext, new AppUserMapper(_mapper));


    private IDirectMessageRepository? _directMessages;
    public virtual IDirectMessageRepository DirectMessages =>
        _directMessages ??= new DirectMessageRepository(UOWDbContext, new DirectMessageMapper(_mapper));

    private IFollowRepository? _follows;
    public virtual IFollowRepository Follows =>
        _follows ??= new FollowRepository(UOWDbContext, new FollowMapper(_mapper));
    
    private ITopicRepository? _topics;
    public virtual ITopicRepository Topics =>
        _topics ??= new TopicRepository(UOWDbContext, new TopicMapper(_mapper));

    private IUserHashtagRepository? _userHashtags;
    public virtual IUserHashtagRepository UserHashtags =>
		_userHashtags ??= new UserHashtagRepository(UOWDbContext, new UserHashtagMappper(_mapper));

    private IUserCommentRepository? _userComments;
    public virtual IUserCommentRepository UserComments =>
        _userComments ??= new UserCommentRepository(UOWDbContext, new UserCommentMapper(_mapper));

    private IUserPostRepository? _userPosts;
    public virtual IUserPostRepository UserPosts =>
        _userPosts ??= new UserPostRepository(UOWDbContext, new UserPostMapper(_mapper));

    private IUserStoriesRepository? _userStories;

    public virtual IUserStoriesRepository UserStories =>
        _userStories ??= new UserStoriesRepository(UOWDbContext, new UserStoriesMapper(_mapper));

    private IUserLikeRepository? _userLikes;

    public virtual IUserLikeRepository UserLikes =>
        _userLikes ??= new UserLikeRepository(UOWDbContext,new UserLikeMapper(_mapper));
}