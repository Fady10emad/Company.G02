
using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.Dots
{
    public class FrogetPasswordDto
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
