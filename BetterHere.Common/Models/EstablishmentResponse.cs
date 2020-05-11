using System;
using System.Collections.Generic;
using System.Text;

namespace BetterHere.Common.Models
{
    public class EstablishmentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoEstablishmentPath { get; set; }

        //TODO: Fix URL when publish in Azure
        public string PictureFullPathEstablishment => string.IsNullOrEmpty(LogoEstablishmentPath)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/establishments/{LogoEstablishmentPath}";

        public UserResponse User { get; set; }

        public List<EstablishmentLocationResponse> EstablishmentLocations { get; set; }
    }
}
