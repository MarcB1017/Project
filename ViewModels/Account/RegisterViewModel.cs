using System.ComponentModel.DataAnnotations;

namespace MVC_CarsForSale.ViewModels.Account
{
    public class RegisterViewModel
    {
        

        [Display(Name = "Email address")]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$", ErrorMessage ="Please use a valid email address.")]

        [Required(ErrorMessage = "Email address is required")]
        public string? EmailAddress { get; set; }

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Your password must contains at least ONE uppercase , ONE lowercase, One special case.")] // One Upper Case , One Lower Case , One Special Case & Min Length Of 8.

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
