using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Data.Entities
{
    public class FoodEntity
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }

        [Display(Name = "Picture Food")]
        public string PicturePathFood { get; set; }

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public TypeFoodEntity TypeFoods { get; set; }

        public UserEntity User { get; set; }
    }
}
