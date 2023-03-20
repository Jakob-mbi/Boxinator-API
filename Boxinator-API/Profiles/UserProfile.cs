using AutoMapper;
using Boxinator_API.DTOs;
using Boxinator_API.Models;

namespace Boxinator_API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>();

            CreateMap<UserPutDto, User>();

            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Shipments, options =>
                options.MapFrom(domain => domain.ShipmentsList.Select(shipment => $"{shipment.Id}").ToList()));
        }
    }
}
