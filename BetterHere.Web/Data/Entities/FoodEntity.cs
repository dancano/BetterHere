using System.ComponentModel.DataAnnotations;

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

        //TODO: Fix URL when publish in Azure
        [Display(Name = "Picture Food")]
        public string PictureFullPathUser => string.IsNullOrEmpty(PicturePathFood)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/foods/{PicturePathFood}";

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public TypeFoodEntity TypeFoods { get; set; }

        public UserEntity User { get; set; }
    }
}
