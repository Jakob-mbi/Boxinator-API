using Boxinator_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Boxinator_API.DTOs.ShipmentDtos
{
    public class GetShipmentDTO
    {
        public int Id { get; set; }

        public string ReciverName { get; set; }

        public int Weight { get; set; }

        public string BoxColor { get; set; }

        public string Destination { get; set; }

        public string? Email { get; set; }

        public string? registerdSender { get; set; }

        public decimal Price { get; set; }
        public List<string> StatusList { get; set; }
    }
}
