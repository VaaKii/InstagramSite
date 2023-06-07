using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<DTO.DirectMessage, Domain.DirectMessage>().ReverseMap();
        CreateMap<DTO.Follow, Domain.Follow>().ReverseMap();
        CreateMap<DTO.Topic, Domain.Topic>().ReverseMap();
        CreateMap<DTO.UserHashtag, Domain.UserHashtag>().ReverseMap();
        CreateMap<DTO.UserComment, Domain.UserComment>().ReverseMap();
        CreateMap<DTO.UserPost, Domain.UserPost>().ReverseMap();
        CreateMap<DTO.UserLike, Domain.UserLike>().ReverseMap();
        CreateMap<DTO.UserStory, Domain.UserStory>().ReverseMap();
        CreateMap<DTO.Identity.AppUser, Domain.Identity.AppUser>().ReverseMap();
    }
}