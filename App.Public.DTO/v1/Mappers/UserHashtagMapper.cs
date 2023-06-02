using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserHashtagMapper: BaseMapper<Public.DTO.v1.UserHashtag, App.BLL.DTO.UserHashtag>
{
    public UserHashtagMapper(IMapper mapper) : base(mapper)
    {
    }
}