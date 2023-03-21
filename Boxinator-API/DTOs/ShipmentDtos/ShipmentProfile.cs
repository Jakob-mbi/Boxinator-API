using AutoMapper;
using Boxinator_API.Models;
using Microsoft.Extensions.Options;

namespace Boxinator_API.DTOs.ShipmentDtos
{
    public class ShipmentProfile : Profile
    {
        ShipmentProfile()
        {
            CreateMap<Shipment, GetShipmentDTO>()
                 .ForMember(dto => dto.registerdSender, options =>
                options.MapFrom(shipment => shipment.User.));
        }
            
        
    }
}
