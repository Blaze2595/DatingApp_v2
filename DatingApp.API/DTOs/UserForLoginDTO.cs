using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForLoginDTO
    {
        [Required(ErrorMessage = "Username cannot be null")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be null")]
        public string Password { get; set; }
    }
}