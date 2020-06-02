﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class EstablishmentRequest
    {
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] PictureEstablishmentArray { get; set; }
    }
}
