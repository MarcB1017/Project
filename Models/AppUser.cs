using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace MVC_CarsForSale.Models
{
    public class AppUser : IdentityUser
    {
        //[Key]
        //public string Id { get; set; }
        // Comment Key and Id after Db Created
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public new string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<Bikes> Bikes { get; set; }
        public ICollection<Cars> Cars { get; set; }
        public ICollection<Vans> Vans { get; set; }
    }
}
