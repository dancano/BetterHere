using BetterHere.Common.Models;
using BetterHere.Web.Data.Entities;

namespace BetterHere.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity slaveEntity);

        EstablishmentResponse ToEstablishmentResponse(EstablishmentEntity establishmentEntity);
    }
}
