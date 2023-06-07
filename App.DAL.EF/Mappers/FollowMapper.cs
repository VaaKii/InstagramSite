using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class FollowMapper : BaseMapper<DTO.Follow, Domain.Follow>
{
	public FollowMapper(IMapper mapper) : base(mapper)
	{
	}
}