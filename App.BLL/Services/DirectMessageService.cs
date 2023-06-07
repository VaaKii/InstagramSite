using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class DirectMessageService : BaseEntityService<DirectMessage, App.DAL.DTO.DirectMessage, IDirectMessageRepository>, 
        IDirectMessageService
{
        public DirectMessageService(IDirectMessageRepository repository, IMapper<DirectMessage, DAL.DTO.DirectMessage> mapper) : base(repository, mapper)
        {
        }
}