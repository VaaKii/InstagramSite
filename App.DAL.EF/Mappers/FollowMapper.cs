using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class FollowMapper : BaseMapper<App.DAL.DTO.Follow, App.Domain.Follow>
{
	public FollowMapper(IMapper mapper) : base(mapper)
	{
	}
}