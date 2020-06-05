using BetterHere.Common.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterHere.Web.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Picture User")]
        public string PicturePathUser { get; set; }

        //TODO: Fix URL when publish in Azure
        [Display(Name = "Picture User")]
        public string PictureFullPathUser => string.IsNullOrEmpty(PicturePathUser)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/users/{PicturePathUser}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "Login Type")]
        public LoginType LoginType { get; set; }


        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public ICollection<EstablishmentEntity> Establishments { get; set; }

        public ICollection<FoodEntity> Foods { get; set; }
    }
}
