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

        public double SourceLatitude { get; set; }

        public double SourceLongitude { get; set; }

        public double TargetLatitude { get; set; }

        public double TargetLongitude { get; set; }

        [Required]
        public float Qualification { get; set; }

        [Required]
        public string Remarks { get; set; }

    }
}
