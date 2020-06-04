using BetterHere.Common.Models;
using System.Collections.Generic;

namespace BetterHere.Common.Helpers
{
    public static class CombosHelper
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Id = 0, Name = "Select your role..." },
                new Role { Id = 1, Name = "Owner" },
                new Role { Id = 2, Name = "User" }
            };
        }

        public static List<CityResponse> GetCities()
        {
            return new List<CityResponse>
            {
                new CityResponse { Id = 1, Name = "Medellin" },
                new CityResponse { Id = 2, Name = "Bogotá" },
                new CityResponse { Id = 3, Name = "Cali" },
                new CityResponse { Id = 4, Name = "Barranquilla" },
                new CityResponse { Id = 5, Name = "Cartagena" },
                new CityResponse { Id = 6, Name = "Pereira" },
                new CityResponse { Id = 7, Name = "Manizales" },
                new CityResponse { Id = 8, Name = "Bucaramanga" }
            };
        }

        public static List<TypeFoodResponse> GetTypeFood()
        {
            return new List<TypeFoodResponse>
            {
                new TypeFoodResponse { Id = 1, FoodTypeName = "Arabic" },
                new TypeFoodResponse { Id = 2, FoodTypeName = "Mexican" },
                new TypeFoodResponse { Id = 3, FoodTypeName = "Germany" },
                new TypeFoodResponse { Id = 4, FoodTypeName = "Chinnese" },
                new TypeFoodResponse { Id = 5, FoodTypeName = "Caribbean" },
            };
        }

        public static List<TypeEstablishmentResponse> GetTypeEstablishment()
        {
            return new List<TypeEstablishmentResponse>
            {
                new TypeEstablishmentResponse { Id = 1, NameType = "Gourmet Food" },
                new TypeEstablishmentResponse { Id = 2, NameType = "Fast Food" },
                new TypeEstablishmentResponse { Id = 3, NameType = "Vegan Food" },

            };
        }
    }
}
