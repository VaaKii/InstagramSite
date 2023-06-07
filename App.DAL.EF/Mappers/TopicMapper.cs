using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TopicMapper: BaseMapper<DTO.Topic, Domain.Topic>
{
    public TopicMapper(IMapper mapper) : base(mapper)
    {
    }
}