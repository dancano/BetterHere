namespace BetterHere.Web.Data.Entities
{
    public class EstablishmentLocationEntity
    {
        public int Id { get; set; }

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        public double TargetLatitude { get; set; }

        public double TargetLongitude { get; set; }

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public EstablishmentEntity Establishments { get; set; }

        public CityEntity Cities { get; set; }
    }
}
