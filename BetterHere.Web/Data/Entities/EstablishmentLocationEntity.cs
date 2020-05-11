using System.Collections.Generic;
using System.Linq;

namespace BetterHere.Web.Data.Entities
{
    public class EstablishmentLocationEntity
    {
        public int Id { get; set; }

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        public double TargetLatitude { get; set; }

        public double TargetLongitude { get; set; }

        public string Remarks { get; set; }

        public EstablishmentEntity Establishments { get; set; }

        public TypeEstablishmentEntity TypeEstablishment { get; set; }

        public CityEntity Cities { get; set; }

        public List<FoodEntity> Foods { get; set; }

        public float? AverageQualification => Foods == null ? 0 : Foods.Average(f => f.Qualification);
    }
}
