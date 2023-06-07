using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserStoriesMapper : BaseMapper<DTO.UserStory, Domain.UserStory>
{
	public UserStoriesMapper(IMapper mapper) : base(mapper)
	{
	}
}