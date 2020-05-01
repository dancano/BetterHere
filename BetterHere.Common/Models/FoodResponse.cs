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
        public string FoodName { get; set; }

        public string PicturePathFood { get; set; }

        //TODO: Fix URL when publish in Azure
        public string PictureFullPathFood => string.IsNullOrEmpty(PicturePathFood)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/foods/{PicturePathFood}";

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public TypeFoodResponse TypeFoods { get; set; }
    }
}
