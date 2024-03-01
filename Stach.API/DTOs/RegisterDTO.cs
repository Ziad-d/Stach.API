using System.ComponentModel.DataAnnotations;

namespace Stach.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression("\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$\"\r\n",
        //    ErrorMessage = "Password must be at least eight characters, one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }
    }
}
