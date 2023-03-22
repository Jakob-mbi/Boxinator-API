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
                 .ForMember(dto => dto.Destination, options => options.MapFrom(shipment => shipment.Destination.Name))
                 .ForMember(dto => dto.registerdSender, options =>
                options.MapFrom(shipment => shipment.User.Sub))
                 .ForMember(dto => dto.StatusList, options =>
               options.MapFrom(list => list.StatusList.Select(status => status.Name).ToList()));
        }
            
        
    }
}
