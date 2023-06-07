using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserStoryMapper: BaseMapper<Public.DTO.v1.UserStory, App.BLL.DTO.UserStory>
{
    public UserStoryMapper(IMapper mapper) : base(mapper)
    {
    }
}