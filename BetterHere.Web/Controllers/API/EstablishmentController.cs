using BetterHere.Common.Models;
using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;
using BetterHere.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetterHere.Web.Controllers.API
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                .Include(e => e.TypeFoods)
                .ThenInclude(e => e.Foods)
                .FirstOrDefaultAsync(e => e.Name == name);

            if (establishmentEntity == null)
            {

            }

            return Ok(_converterHelper.ToEstablishmentResponse(establishmentEntity));
        }
    }
}