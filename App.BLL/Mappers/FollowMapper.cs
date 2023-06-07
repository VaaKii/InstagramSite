using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class FollowMapper: BaseMapper<DTO.Follow, App.DAL.DTO.Follow>
{
    public FollowMapper(IMapper mapper) : base(mapper)
    {
    }
}