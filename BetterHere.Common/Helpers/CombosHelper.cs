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
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Owner" },
                new Role { Id = 3, Name = "User" }
            };
        }
    }
}
