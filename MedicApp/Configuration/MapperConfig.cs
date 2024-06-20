using AutoMapper;
using MedicApp.Data;
using MedicApp.DTO;
using MedicApp.Models;

namespace MedicApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<User, UserSignupDTO>().ReverseMap();;
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }


    }
}
