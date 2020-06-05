using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Models
{
    public class CreateStablishmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile PictureFile { get; set; }

        public string PicturePath { get; set; }
    }
}
