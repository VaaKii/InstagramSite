using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class FollowMapper: BaseMapper<App.BLL.DTO.Follow, App.DAL.DTO.Follow>
{
    public FollowMapper(IMapper mapper) : base(mapper)
    {
    }
}