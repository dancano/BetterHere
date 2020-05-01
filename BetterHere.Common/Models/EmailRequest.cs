using System.ComponentModel.DataAnnotations;

namespace BetterHere.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
