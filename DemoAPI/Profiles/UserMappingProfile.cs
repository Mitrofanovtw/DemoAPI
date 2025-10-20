using AutoMapper;
using DemoAPI.Models;
using DemoAPI.Models.DTO;
using static DemoAPI.Models.DTO.CreateUserDTO;

namespace DemoAPI.Profiles
{
        public class UserMappingProfile : Profile
        {
            public UserMappingProfile()
            {

            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.RegistrationDateFormatted,
                opt => opt.MapFrom(src => DateTime.Now.ToString("dd.MM.yyyy")));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Login,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email));
        }
        }
    }
