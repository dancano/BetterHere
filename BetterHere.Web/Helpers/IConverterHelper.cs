using BetterHere.Common.Models;
using BetterHere.Web.Data.Entities;
using System.Collections.Generic;

namespace BetterHere.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity slaveEntity);

        EstablishmentResponse ToEstablishmentResponse(EstablishmentEntity establishmentEntity);

        List<EstablishmentResponse> ToEstablishmentResponse(List<EstablishmentEntity> establishmentEntity);

        EstablishmentLocationResponse ToEstablishmentLocationResponse(EstablishmentLocationEntity establishmentLocationEntity);

        List<EstablishmentLocationResponse> ToEstablishmentLocationResponse(List<EstablishmentLocationEntity> establishmentLocationEntity);

        CityResponse ToCityResponse(CityEntity cityEntity);
        
        TypeEstablishmentResponse ToTypeEstablishmentResponse(TypeEstablishmentEntity typeEstablishmentEntity);

        TypeFoodResponse ToTypeFoodResponse(TypeFoodEntity typeFoodEntity);

        FoodResponse ToFoodResponse(FoodEntity foodEntity);

        List<FoodResponse> ToFoodResponse(List<FoodEntity> foodEntity);
    }
}
