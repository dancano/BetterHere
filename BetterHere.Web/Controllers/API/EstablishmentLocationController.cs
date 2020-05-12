using BetterHere.Common.Models;
using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;
using BetterHere.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Controllers.API
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EstablishmentLocationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public EstablishmentLocationController(
            DataContext context,
            IUserHelper userHelper,
            IConverterHelper converterHelper
            )
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetEstablishmentLocations")]
        public async Task<IActionResult> GetEstablishmentLocations([FromBody] EstablishmentLocationRequest establishmentLocationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<EstablishmentLocationEntity> establishmentLocationEntity = await _context.EstablishmentLocations
                .Include(m => m.Establishments)
                .Include(m => m.Foods)
                .ThenInclude(m => m.TypeFoods)
                .Include(m => m.Foods)
                .ThenInclude(m => m.User)
                .Include(m => m.Cities)
                .Include(m => m.TypeEstablishment)
                .Where(e => e.Establishments.Id == establishmentLocationRequest.IdEstablishment)
                .ToListAsync();

            if (establishmentLocationEntity == null)
            {

            }

            return Ok(_converterHelper.ToEstablishmentLocationResponse(establishmentLocationEntity));
        }

        [HttpPost]
        [Route("PostEstablishmentLocation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostEstablishmentLocation([FromBody] EstablishmentLocationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            EstablishmentEntity establishment = await _context.Establishments.FirstOrDefaultAsync(e => e.Id == request.IdEstablishment);
            CityEntity city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == request.IdCity);
            TypeEstablishmentEntity typeEstablishment = await _context.TypeEstablishments.FirstOrDefaultAsync(te => te.Id == request.IdTypeEstablishment);

            List<EstablishmentLocationEntity> establishmentLocationEntity = await _context.EstablishmentLocations
                .Include(m => m.Establishments)
                .Include(m => m.Foods)
                .ThenInclude(m => m.TypeFoods)
                .Include(m => m.Foods)
                .ThenInclude(m => m.User)
                .Include(m => m.Cities)
                .Include(m => m.TypeEstablishment)
                .Where(e => e.Establishments.Id == request.IdEstablishment)
                .ToListAsync();

            var LatitudeValidation = establishmentLocationEntity.FirstOrDefault(el=> el.SourceLatitude == request.SourceLatitude);
            var LongitudeValidation = establishmentLocationEntity.FirstOrDefault(el => el.SourceLongitude == request.SourceLongitude);

            if (LatitudeValidation != null & LongitudeValidation != null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Establishment location exist"
                });
            }

            EstablishmentLocationEntity establishmentLocation = new EstablishmentLocationEntity
            {
                SourceLatitude = request.SourceLatitude,
                SourceLongitude = request.SourceLongitude,
                TargetLatitude = request.TargetLatitude,
                TargetLongitude = request.TargetLongitude,
                Remarks = request.Remarks,
                Establishments = establishment,
                Cities = city,
                TypeEstablishment = typeEstablishment
            };

            _context.EstablishmentLocations.Add(establishmentLocation);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToEstablishmentLocationResponse(establishmentLocation));

        }
    }
}