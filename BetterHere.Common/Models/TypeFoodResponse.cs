using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class TypeFoodResponse
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        public string FoodType { get; set; }

        public EstablishmentResponse Establishments { get; set; }

        public ICollection<FoodResponse> Foods { get; set; }
    }
}
