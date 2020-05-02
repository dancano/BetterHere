using BetterHere.Common.Models;
using BetterHere.Web.Data.Entities;
using System.Linq;

namespace BetterHere.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public UserResponse ToUserResponse(UserEntity userEntity)
        {
            return new UserResponse
            {
                Id = userEntity.Id,
                Document = userEntity.Document,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                Address = userEntity.Address,
                PicturePathUser = userEntity.PicturePathUser
            };
        }

        public EstablishmentResponse ToEstablishmentResponse(EstablishmentEntity establishmentEntity)
        {
            return new EstablishmentResponse
            {
                Id = establishmentEntity.Id,
                Name = establishmentEntity.Name,
                LogoEstablishmentPath = establishmentEntity.LogoEstablishmentPath,
                EstablishmentLocations = establishmentEntity.EstablishmentLocations?.Select(
                    el => new EstablishmentLocationResponse
                    {
                        Id = el.Id,
                        SourceLatitude = el.SourceLatitude,
                        SourceLongitude = el.SourceLongitude,
                        TargetLatitude = el.TargetLatitude,
                        TargetLongitude = el.TargetLongitude,
                        Qualification = el.Qualification,
                        Remarks = el.Remarks
                    }).ToList(),
                TypeFoods = establishmentEntity.TypeFoods?.Select(
                    tf => new TypeFoodResponse
                    {
                        Id = tf.Id,
                        FoodTypeName = tf.FoodTypeName,
                        Foods = tf.Foods?.Select(
                            f => new FoodResponse
                            {
                                Id = f.Id,
                                FoodName = f.FoodName,
                                PicturePathFood = f.PicturePathFood,
                                Qualification = f.Qualification,
                                Remarks = f.Remarks
                            }).ToList()
                    }).ToList()
            };
        }
    }
}
