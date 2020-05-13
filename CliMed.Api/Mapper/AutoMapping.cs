using AutoMapper;
using CliMed.Api.Dto;
using CliMed.Api.Models;

namespace CliMed.Api.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
