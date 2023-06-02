using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class FollowMapper: BaseMapper<Public.DTO.v1.Follow, App.BLL.DTO.Follow>
{
    public FollowMapper(IMapper mapper) : base(mapper)
    {
    }
}