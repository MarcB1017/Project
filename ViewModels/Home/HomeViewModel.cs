namespace MVC_CarsForSale.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<Models.Cars> Cars { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
