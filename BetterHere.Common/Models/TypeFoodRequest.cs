using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class TypeFoodRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        public string FoodTypeName { get; set; }

    }
}
