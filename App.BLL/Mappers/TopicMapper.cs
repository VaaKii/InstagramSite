using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TopicMapper: BaseMapper<DTO.Topic, App.DAL.DTO.Topic>
{
    public TopicMapper(IMapper mapper) : base(mapper)
    {
    }
}