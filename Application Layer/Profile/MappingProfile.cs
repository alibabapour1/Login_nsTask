using AutoMapper;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginModel, Users>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()); 
    }
}
