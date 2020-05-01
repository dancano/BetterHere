using System.Collections.Generic;

namespace BetterHere.Web.Data.Entities
{
    public class CityEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EstablishmentLocationEntity> EstablishmentLocations { get; set; }
    }
}
