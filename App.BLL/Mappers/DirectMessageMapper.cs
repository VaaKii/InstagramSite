using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class DirectMessageMapper : BaseMapper<DTO.DirectMessage, App.DAL.DTO.DirectMessage>
{
    public DirectMessageMapper(IMapper mapper) : base(mapper)
    {
    }
}