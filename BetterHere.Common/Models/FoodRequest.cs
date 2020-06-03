using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BetterHere.Common.Models
{
    public class FoodRequest
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "The {0} field must have {1} characters.")]
        public string FoodName { get; set; }

        public string PicturePathFood { get; set; }

        public float Qualification { get; set; }

        public string Remarks { get; set; }

        public int EstablishmentLocationId { get; set; }

        public int TypeFoodsId { get; set; }

        public byte[] PictureFoodArray { get; set; }

        public Guid UserId { get; set; }
    }
}
