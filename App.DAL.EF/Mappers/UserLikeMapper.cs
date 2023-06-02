using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserLikeMapper : BaseMapper<App.DAL.DTO.UserLike, App.Domain.UserLike>
{
	public UserLikeMapper(IMapper mapper) : base(mapper)
	{
	}
}