using BetterHere.Common.Models;
using BetterHere.Web.Data.Entities;

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
    }
}
