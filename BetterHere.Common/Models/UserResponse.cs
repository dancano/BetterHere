using BetterHere.Common.Enum;
using System.Collections.Generic;

namespace BetterHere.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PicturePathUser { get; set; }

        //TODO: Fix URL when publish in Azure
        public string PictureFullPathUser => string.IsNullOrEmpty(PicturePathUser)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/users/{PicturePathUser}";

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public List<EstablishmentResponse> Establishments { get; set; }

        public List<FoodResponse> Foods { get; set; }
    }
}
