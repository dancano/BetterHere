using System;
using System.Collections.Generic;
using System.Text;

namespace BetterHere.Common.Models
{
    public class CityResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EstablishmentLocationResponse> EstablishmentLocations { get; set; }
    }
}
