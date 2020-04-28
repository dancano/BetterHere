﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Data.Entities
{
    public class EstablishmentEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Logo Establishment")]
        public string LogoEstablishmentPath { get; set; }

        public UserEntity User { get; set; }

        public TypeEstablishmentEntity TypeEstablishment { get; set; }

        public List<EstablishmentLocationEntity> EstablishmentLocations { get; set; }

        public List<TypeFoodEntity> TypeFoods { get; set; }

    }
}
