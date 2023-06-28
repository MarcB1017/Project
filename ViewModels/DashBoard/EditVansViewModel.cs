using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.ViewModels.DashBoard
{
    public class EditVansViewModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Milage { get; set; }
        public string VanBrand { get; set; }
        public string VanModel { get; set; }
        public int VanYear { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string? AppUserId { get; set; }
    }
}
