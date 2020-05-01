using System;
using System.Collections.Generic;
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

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public EstablishmentResponse Establishments { get; set; }
    }
}
