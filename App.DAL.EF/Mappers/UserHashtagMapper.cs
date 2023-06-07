using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserHashtagMappper : BaseMapper<DTO.UserHashtag, Domain.UserHashtag>
{
	public UserHashtagMappper(IMapper mapper) : base(mapper)
	{
	}
}