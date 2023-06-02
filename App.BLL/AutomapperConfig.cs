using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.DirectMessage, App.DAL.DTO.DirectMessage>().ReverseMap();
        CreateMap<App.BLL.DTO.Follow, App.DAL.DTO.Follow>().ReverseMap();
        CreateMap<App.BLL.DTO.Topic, App.DAL.DTO.Topic>().ReverseMap();
        CreateMap<App.BLL.DTO.UserHashtag, App.DAL.DTO.UserHashtag>().ReverseMap();
        CreateMap<App.BLL.DTO.UserComment, App.DAL.DTO.UserComment>().ReverseMap();
        CreateMap<App.BLL.DTO.UserPost, App.DAL.DTO.UserPost>().ReverseMap();
        CreateMap<App.BLL.DTO.UserLike, App.DAL.DTO.UserLike>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.UserStories,App.DAL.DTO.UserStories>().ReverseMap();
    }
}