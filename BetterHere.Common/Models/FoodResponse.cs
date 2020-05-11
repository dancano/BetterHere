using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class FoodResponse
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }

        [Display(Name = "Picture Food")]
        public string PicturePathFood { get; set; }

        //TODO: Fix URL when publish in Azure
        [Display(Name = "Picture Food")]
        public string PictureFullPathFood => string.IsNullOrEmpty(PicturePathFood)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/foods/{PicturePathFood}";

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public string EstablishmentLocationId { get; set; }

        public EstablishmentLocationResponse EstablishmentLocations { get; set; }

        public TypeFoodResponse TypeFoods { get; set; }

        public UserResponse User { get; set; }
    }
}
