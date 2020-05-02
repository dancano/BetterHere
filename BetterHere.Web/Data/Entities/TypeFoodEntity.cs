using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterHere.Web.Data.Entities
{
    public class TypeFoodEntity
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        [Display(Name = "Food Type")]
        public string FoodTypeName { get; set; }

        public ICollection<FoodEntity> Foods { get; set; }
    }
}
