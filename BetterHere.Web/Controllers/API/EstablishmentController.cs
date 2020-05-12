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
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;

        public EstablishmentController(
            DataContext context,
            IBlobHelper blobHelper,
            IUserHelper userHelper,
            IConverterHelper converterHelper
            )
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
        }

        // GET: api/Trips
        [HttpGet]
        public IEnumerable<EstablishmentEntity> GetEstablishments()
        {
            return _context.Establishments;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetEstablishmentEntity([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EstablishmentEntity establishmentEntity = await _context.Establishments
                .Include(e => e.EstablishmentLocations)
                .FirstOrDefaultAsync(e => e.Name == name);

            if (establishmentEntity == null)
            {

            }

            return Ok(_converterHelper.ToEstablishmentResponse(establishmentEntity));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostEstablishment([FromBody] EstablishmentRequest request)
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
            
            UserEntity userEntity = await _userHelper.GetUserAsync(request.UserId);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            EstablishmentEntity establishment = await _context.Establishments.FirstOrDefaultAsync(e => e.Name == request.Name);
            if (establishment != null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Establishment exist"
                });
            }

            string picturePath = string.Empty;
            if (request.PictureEstablishmentArray != null && request.PictureEstablishmentArray.Length > 0)
            {
                picturePath = await _blobHelper.UploadBlobAsync(request.PictureEstablishmentArray, "establishments");
            }

            establishment = new EstablishmentEntity
            {
                Name = request.Name,
                LogoEstablishmentPath = picturePath,
                User = userEntity
            };

            _context.Establishments.Add(establishment);
            await _context.SaveChangesAsync();

            return Ok(_converterHelper.ToEstablishmentResponse(establishment));
        }
    }
}