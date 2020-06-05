using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterHere.Web.Data.Entities
{
    public class EstablishmentEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Logo Establishment")]
        public string LogoEstablishmentPath { get; set; }

        //TODO: Fix URL when publish in Azure
        [Display(Name = "Logo Establishment")]
        public string PictureFullPathEstablishment => string.IsNullOrEmpty(LogoEstablishmentPath)
        ? "https://JICtravelweb.azurewebsites.net//images/noimage.png"
        : $"https://betterhere.blob.core.windows.net/establishments/{LogoEstablishmentPath}";

        public UserEntity User { get; set; }


        public List<EstablishmentLocationEntity> EstablishmentLocations { get; set; }

    }
}
