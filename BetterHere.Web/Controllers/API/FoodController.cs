using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterHere.Common.Models;
using BetterHere.Web.Data;
using BetterHere.Web.Data.Entities;
using BetterHere.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterHere.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;

        public FoodController(
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

        [HttpPost]
        [Route("GetFoods")]
        public async Task<IActionResult> GetFoods([FromBody] FoodRequest foodRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<FoodEntity> foodEntity = await _context.Foods
                .Include(m => m.TypeFoods)
                .Include(m => m.User)
                .Where(e => e.EstablishmentLocations.Id == foodRequest.EstablishmentLocationId)
                .ToListAsync();

            if (foodEntity == null)
            {

            }

            return Ok(_converterHelper.ToFoodResponse(foodEntity));
        }

        [HttpPost]
        [Route("PostFood")]
        public async Task<IActionResult> PostFood([FromBody] FoodRequest request)
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

            EstablishmentLocationEntity establishmentLocationEntity = await _context
                .EstablishmentLocations
                .FirstOrDefaultAsync(el => el.Id == request.EstablishmentLocationId);

            FoodEntity foodEntity = await _context.Foods.FirstOrDefaultAsync(f => f.FoodName == request.FoodName);
            
            TypeFoodEntity typeFoodEntity = await _context.TypeFoods.FirstOrDefaultAsync(f => f.Id == request.TypeFoodsId);

            UserEntity userEntity = await _userHelper.GetUserAsync(request.UserId);

            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            if (foodEntity != null)
            {
                if (establishmentLocationEntity.Id == foodEntity.EstablishmentLocations.Id)
                {
                    return BadRequest(new Response
                    {
                        IsSuccess = false,
                        Message = "This food exist in this place"
                    });
                }
            }

            FoodEntity food = new FoodEntity
            {
                FoodName = request.FoodName,
                PicturePathFood = request.PicturePathFood,
                Qualification = request.Qualification,
                Remarks = request.Remarks,
                TypeFoods = typeFoodEntity,
                EstablishmentLocationId = establishmentLocationEntity.Id.ToString(),
                EstablishmentLocations = establishmentLocationEntity,
                User = userEntity
            };

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToFoodResponse(food));

        }

    }
}