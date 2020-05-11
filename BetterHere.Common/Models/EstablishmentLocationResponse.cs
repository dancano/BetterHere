using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterHere.Common.Models
{
    public class EstablishmentLocationResponse
    {
        public int Id { get; set; }

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        public double TargetLatitude { get; set; }

        public double TargetLongitude { get; set; }

        public string Remarks { get; set; }

        public EstablishmentResponse Establishments { get; set; }

        public TypeEstablishmentResponse TypeEstablishment { get; set; }

        public CityResponse Cities { get; set; }

        public List<FoodResponse> Foods { get; set; }

        public float AverageQualification => Foods == null ? 0 : Foods.Average(f => f.Qualification);
    }
}
