using AutoMapper;
using MedicApp.Data;
using MedicApp.DTO;

namespace MedicApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<User, UserSignupDTO>().ReverseMap();
            CreateMap<User, UserPatchDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}
