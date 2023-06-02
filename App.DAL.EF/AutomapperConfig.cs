using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.DAL.DTO.DirectMessage, App.Domain.DirectMessage>().ReverseMap();
        CreateMap<App.DAL.DTO.Follow, App.Domain.Follow>().ReverseMap();
        CreateMap<App.DAL.DTO.Topic, App.Domain.Topic>().ReverseMap();
        CreateMap<App.DAL.DTO.UserHashtag, App.Domain.UserHashtag>().ReverseMap();
        CreateMap<App.DAL.DTO.UserComment, App.Domain.UserComment>().ReverseMap();
        CreateMap<App.DAL.DTO.UserPost, App.Domain.UserPost>().ReverseMap();
        CreateMap<App.DAL.DTO.UserLike, App.Domain.UserLike>().ReverseMap();
        CreateMap<App.DAL.DTO.UserStories, App.Domain.UserStories>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
    }
}