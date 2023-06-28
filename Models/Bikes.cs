using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_CarsForSale.Models
{
    public class Bikes
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public int Milage { get; set; }
        public string BikeBrand { get; set; }
        public string BikeModel { get; set; }
        public int BikeYear { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
