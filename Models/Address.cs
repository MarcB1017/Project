using System.ComponentModel.DataAnnotations;

namespace MVC_CarsForSale.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
