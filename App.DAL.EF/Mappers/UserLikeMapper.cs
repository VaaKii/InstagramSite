using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserLikeMapper : BaseMapper<DTO.UserLike, Domain.UserLike>
{
	public UserLikeMapper(IMapper mapper) : base(mapper)
	{
	}
}