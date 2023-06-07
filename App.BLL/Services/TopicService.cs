using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class TopicService : BaseEntityService<Topic, App.DAL.DTO.Topic, ITopicRepository>,
    ITopicService
{
    public TopicService(ITopicRepository repository, IMapper<Topic, DAL.DTO.Topic> mapper) : base(repository, mapper)
    {
    }
}