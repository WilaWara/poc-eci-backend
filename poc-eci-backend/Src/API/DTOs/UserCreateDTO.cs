using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "The user name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The user email is required")]
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The user password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The user role is required")]
        public string Role { get; set; }
    }
}
