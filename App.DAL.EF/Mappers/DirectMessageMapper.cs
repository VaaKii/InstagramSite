using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class DirectMessageMapper : BaseMapper<DTO.DirectMessage, Domain.DirectMessage>
{
	public DirectMessageMapper(IMapper mapper) : base(mapper)
	{
	}
}