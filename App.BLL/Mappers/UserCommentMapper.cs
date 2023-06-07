using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserCommentMapper: BaseMapper<DTO.UserComment, App.DAL.DTO.UserComment>
{
    public UserCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}