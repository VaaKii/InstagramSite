using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserStoriesMapper : BaseMapper<App.DAL.DTO.UserStories, App.Domain.UserStories>
{
	public UserStoriesMapper(IMapper mapper) : base(mapper)
	{
	}
}