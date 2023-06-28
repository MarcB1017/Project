namespace MVC_CarsForSale.ViewModels.Users
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Location => (City, Country) switch
        {
            (string city, string country) => $"{city}, {country}",
            (string city, null) => city,
            (null, string country) => country,
            (null, null) => "",
        };

        public string PhoneNumber { get; set; }
    }
}
