using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserStoriesMapper: BaseMapper<DTO.UserStory, App.DAL.DTO.UserStory>
{
    public UserStoriesMapper(IMapper mapper) : base(mapper)
    {
    }
}