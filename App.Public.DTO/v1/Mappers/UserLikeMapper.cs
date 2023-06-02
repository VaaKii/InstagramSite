using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserLikeMapper: BaseMapper<Public.DTO.v1.UserLike, App.BLL.DTO.UserLike>
{
    public UserLikeMapper(IMapper mapper) : base(mapper)
    {
    }
}