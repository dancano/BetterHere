using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class EstablishmentLocationRequest
    {
        [Required]
        public int IdEstablishment { get; set; }

        public int IdCity { get; set; }

        public int IdTypeEstablishment { get; set; }

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        public double TargetLatitude { get; set; }

        public double TargetLongitude { get; set; }

        public string Remarks { get; set; }

    }
}
