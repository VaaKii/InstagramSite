using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserHashtagMapper: BaseMapper<DTO.UserHashtag, App.DAL.DTO.UserHashtag>
{
    public UserHashtagMapper(IMapper mapper) : base(mapper)
    {
    }
}