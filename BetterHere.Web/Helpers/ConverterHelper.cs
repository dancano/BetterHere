using BetterHere.Common.Models;
using BetterHere.Web.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Collections.Generic;
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
                        Remarks = el.Remarks,
                        Cities = ToCityResponse(el.Cities),
                        TypeEstablishment = ToTypeEstablishmentResponse(el.TypeEstablishment),
                        Foods = el.Foods?.Select(f => new FoodResponse
                        {
                            Id = f.Id,
                            FoodName = f.FoodName,
                            Qualification = f.Qualification,
                            Remarks = f.Remarks,
                            PicturePathFood = f.PicturePathFood,
                            EstablishmentLocationId = el.Id.ToString(),                  
                            TypeFoods = ToTypeFoodResponse(f.TypeFoods),
                            User = ToUserResponse(f.User)
                        }).ToList()
                    }).ToList()
            };
        }

        public List<EstablishmentResponse> ToEstablishmentResponse(List<EstablishmentEntity> establishmentEntity)
        {
            return establishmentEntity.Select( e => new EstablishmentResponse
            {
                Id = e.Id,
                Name = e.Name,
                LogoEstablishmentPath = e.LogoEstablishmentPath,
                EstablishmentLocations = e.EstablishmentLocations?.Select(
                    el => new EstablishmentLocationResponse
                    {
                        Id = el.Id,
                        SourceLatitude = el.SourceLatitude,
                        SourceLongitude = el.SourceLongitude,
                        TargetLatitude = el.TargetLatitude,
                        TargetLongitude = el.TargetLongitude,
                        Remarks = el.Remarks
                    }).ToList()
            }).ToList();
        }

        public EstablishmentLocationResponse ToEstablishmentLocationResponse(EstablishmentLocationEntity establishmentLocationEntity)
        {
            return new EstablishmentLocationResponse
            {
                Id = establishmentLocationEntity.Id,
                Remarks = establishmentLocationEntity.Remarks,
                SourceLatitude = establishmentLocationEntity.SourceLatitude,
                TargetLatitude = establishmentLocationEntity.TargetLatitude,
                SourceLongitude = establishmentLocationEntity.SourceLatitude,
                TargetLongitude = establishmentLocationEntity.TargetLongitude,
                Establishments = ToEstablishmentResponse(establishmentLocationEntity.Establishments),
                Cities = ToCityResponse(establishmentLocationEntity.Cities),
                TypeEstablishment = ToTypeEstablishmentResponse(establishmentLocationEntity.TypeEstablishment),
                Foods = establishmentLocationEntity.Foods?.Select(f => new FoodResponse
                {
                    Id = f.Id,
                    FoodName = f.FoodName,
                    PicturePathFood = f.PicturePathFood,
                    Qualification = f.Qualification,
                    Remarks = f.Remarks,
                    User = ToUserResponse(f.User),
                    TypeFoods = ToTypeFoodResponse(f.TypeFoods)
                }).ToList()
            };
        }

        public List<EstablishmentLocationResponse> ToEstablishmentLocationResponse(List<EstablishmentLocationEntity> establishmentLocationEntity)
        {
            return establishmentLocationEntity.Select( el => new EstablishmentLocationResponse
            {
                Id = el.Id,
                Remarks = el.Remarks,
                SourceLatitude = el.SourceLatitude,
                TargetLatitude = el.TargetLatitude,
                SourceLongitude = el.SourceLatitude,
                TargetLongitude = el.TargetLongitude,
                Cities = ToCityResponse(el.Cities),
                TypeEstablishment = ToTypeEstablishmentResponse(el.TypeEstablishment)
            }).ToList();
        }

        public CityResponse ToCityResponse(CityEntity cityEntity)
        {
            return new CityResponse
            {
                Id = cityEntity.Id,
                Name = cityEntity.Name
            };
        }

        public TypeEstablishmentResponse ToTypeEstablishmentResponse(TypeEstablishmentEntity typeEstablishmentEntity)
        {
            return new TypeEstablishmentResponse
            {
                Id = typeEstablishmentEntity.Id,
                NameType = typeEstablishmentEntity.NameType
            };
        }

        public TypeFoodResponse ToTypeFoodResponse(TypeFoodEntity typeFoodEntity)
        {
            return new TypeFoodResponse
            {
                Id = typeFoodEntity.Id,
                FoodTypeName = typeFoodEntity.FoodTypeName
            };
        }

        public FoodResponse ToFoodResponse(FoodEntity foodEntity)
        {
            return new FoodResponse
            {
                Id = foodEntity.Id,
                FoodName = foodEntity.FoodName,
                PicturePathFood = foodEntity.PicturePathFood,
                Qualification = foodEntity.Qualification,
                Remarks = foodEntity.Remarks,
                EstablishmentLocationId = foodEntity.EstablishmentLocationId,
                TypeFoods = ToTypeFoodResponse(foodEntity.TypeFoods),
                User = ToUserResponse(foodEntity.User)
            };
        }

        public List<FoodResponse> ToFoodResponse(List<FoodEntity> foodEntity)
        {
            return foodEntity.Select(f => new FoodResponse
            {
                Id = f.Id,
                FoodName = f.FoodName,
                PicturePathFood = f.PicturePathFood,
                Qualification = f.Qualification,
                Remarks = f.Remarks,
                TypeFoods = ToTypeFoodResponse(f.TypeFoods),
                User = ToUserResponse(f.User)
            }).ToList();
        }
    }
}
