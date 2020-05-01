using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class FoodRequest
    {
        [Required]
        public int IdTypeFood { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        public string FoodName { get; set; }

        public byte[] PictureUserArray { get; set; }

        [Required]
        public float Qualification { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Remarks { get; set; }
    }
}
