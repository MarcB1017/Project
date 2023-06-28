namespace MVC_CarsForSale.ViewModels.DashBoard
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public IFormFile Image { get; set; }
    }
}
