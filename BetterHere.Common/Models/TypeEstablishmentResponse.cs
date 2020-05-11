using System;
using System.Collections.Generic;
using System.Text;

namespace BetterHere.Common.Models
{
    public class TypeEstablishmentResponse
    {
        public int Id { get; set; }

        public string NameType { get; set; }

        public List<EstablishmentLocationResponse> EstablishmentLocations { get; set; }
    }
}
