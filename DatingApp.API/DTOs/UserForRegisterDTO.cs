using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForRegisterDTO
    {
        [Required(ErrorMessage = "Username Cannot be null")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password Cannot be null")]
        [StringLength(15,MinimumLength = 5, ErrorMessage = "Password should be 5 to 15 characters long")]
        public string Password { get; set; }
    }
}