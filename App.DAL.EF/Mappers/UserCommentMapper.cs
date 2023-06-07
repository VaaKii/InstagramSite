using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserCommentMapper: BaseMapper<DTO.UserComment, Domain.UserComment>
{
    public UserCommentMapper(IMapper mapper) : base(mapper)
    {
    }
}