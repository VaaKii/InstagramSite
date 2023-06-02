using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class DirectMessageMapper : BaseMapper<App.DAL.DTO.DirectMessage, App.Domain.DirectMessage>
{
	public DirectMessageMapper(IMapper mapper) : base(mapper)
	{
	}
}