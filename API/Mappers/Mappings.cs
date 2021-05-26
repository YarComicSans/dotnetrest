using API.Models;
using API.Models.Dtos;
using AutoMapper;

namespace API.Mappers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Deadlock, DeadlockDto>().ReverseMap().ForSourceMember(src => src.Id == null, opt => opt.DoNotValidate());
        }
    }
}
