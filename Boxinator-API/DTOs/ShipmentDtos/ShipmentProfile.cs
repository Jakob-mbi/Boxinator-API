using AutoMapper;
using Boxinator_API.Models;
using Microsoft.Extensions.Options;

namespace Boxinator_API.DTOs.ShipmentDtos
{
    public class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            CreateMap<Shipment, GetShipmentDTO>()
                 .ForMember(dto => dto.StatusList, options =>
               options.MapFrom(model => model.StatusList.Select(status => status.Name).ToList()))
                 .ForMember(dto => dto.Destination, options => options.MapFrom(model => model.Destination.Name));
            CreateMap<PutShipmentDTO, Shipment>();
            CreateMap<PostShipmentDTO, Shipment>();
        }
            
        
    }
}
