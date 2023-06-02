using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserStoriesMapper: BaseMapper<Public.DTO.v1.UserStories, App.BLL.DTO.UserStories>
{
    public UserStoriesMapper(IMapper mapper) : base(mapper)
    {
    }
}