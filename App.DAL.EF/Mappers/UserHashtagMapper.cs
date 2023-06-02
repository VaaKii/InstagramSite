using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserHashtagMappper : BaseMapper<App.DAL.DTO.UserHashtag, App.Domain.UserHashtag>
{
	public UserHashtagMappper(IMapper mapper) : base(mapper)
	{
	}
}