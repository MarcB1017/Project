using MVC_CarsForSale.Models;

namespace MVC_CarsForSale.ViewModels.Vans
{
    public class CreateVanViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string VanBrand { get; set; }
        public string VanModel { get; set; }
        public int VanYear { get; set; }
        public int Price { get; set; }
        public int Milage { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public string AppUserId { get; set; }
    }
}
