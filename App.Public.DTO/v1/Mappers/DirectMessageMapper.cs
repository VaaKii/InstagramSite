using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class DirectMessageMapper : BaseMapper<Public.DTO.v1.DirectMessage, App.BLL.DTO.DirectMessage>
{
    public DirectMessageMapper(IMapper mapper) : base(mapper)
    {
    }
}