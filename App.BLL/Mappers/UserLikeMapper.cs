using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserLikeMapper: BaseMapper<DTO.UserLike, App.DAL.DTO.UserLike>
{
    public UserLikeMapper(IMapper mapper) : base(mapper)
    {
    }
}