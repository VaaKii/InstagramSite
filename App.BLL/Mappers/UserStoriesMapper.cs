using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserStoriesMapper: BaseMapper<App.BLL.DTO.UserStories, App.DAL.DTO.UserStories>
{
    public UserStoriesMapper(IMapper mapper) : base(mapper)
    {
    }
}