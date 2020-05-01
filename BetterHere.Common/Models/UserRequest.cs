using System.ComponentModel.DataAnnotations;

namespace BetterHere.Common.Models
{
    public class UserRequest
    {
        [Required]
        public string Document { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public int UserTypeId { get; set; } // 1 => Owner : 2 => User 

        public byte[] PictureUserArray { get; set; }

        public string PasswordConfirm { get; set; }
    }
}
