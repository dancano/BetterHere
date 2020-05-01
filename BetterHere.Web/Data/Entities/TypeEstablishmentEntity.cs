using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterHere.Web.Data.Entities
{
    public class TypeEstablishmentEntity
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        [Display(Name = "Establishment Type")]
        public string NameType { get; set; }

        public ICollection<EstablishmentEntity> Establishments { get; set; }
    }
}
