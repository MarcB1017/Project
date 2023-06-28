using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.ViewModels.DashBoard
{
    public class EditCarsViewModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Milage { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string? AppUserId { get; set; }
    }
}
