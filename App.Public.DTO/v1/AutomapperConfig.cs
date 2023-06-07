using AutoMapper;

namespace App.Public.DTO.v1;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.Public.DTO.v1.DirectMessage, App.BLL.DTO.DirectMessage>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Follow, App.BLL.DTO.Follow>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserHashtag, App.BLL.DTO.UserHashtag>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Topic, App.BLL.DTO.Topic>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserHashtag, App.BLL.DTO.UserHashtag>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserComment, App.BLL.DTO.UserComment>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserPost, App.BLL.DTO.UserPost>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserLike, App.BLL.DTO.UserLike>().ReverseMap();
        CreateMap<App.Public.DTO.v1.UserStory, App.BLL.DTO.UserStory>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Identity.AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
    }
}