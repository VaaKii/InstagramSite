using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<DTO.DirectMessage, App.DAL.DTO.DirectMessage>().ReverseMap();
        CreateMap<DTO.Follow, App.DAL.DTO.Follow>().ReverseMap();
        CreateMap<DTO.Topic, App.DAL.DTO.Topic>().ReverseMap();
        CreateMap<DTO.UserHashtag, App.DAL.DTO.UserHashtag>().ReverseMap();
        CreateMap<DTO.UserComment, App.DAL.DTO.UserComment>().ReverseMap();
        CreateMap<DTO.UserPost, App.DAL.DTO.UserPost>().ReverseMap();
        CreateMap<DTO.UserLike, App.DAL.DTO.UserLike>().ReverseMap();
        CreateMap<DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<DTO.UserStory,App.DAL.DTO.UserStory>().ReverseMap();
    }
}