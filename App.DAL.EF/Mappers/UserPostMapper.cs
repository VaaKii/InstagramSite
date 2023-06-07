using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserPostMapper: BaseMapper<DTO.UserPost, Domain.UserPost>
{
    public UserPostMapper(IMapper mapper) : base(mapper)
    {
    }
}