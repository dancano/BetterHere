using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BetterHere.Web.Helpers
{
    public class ComboHelper : IComboHelper
    {

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "[Select a role...]" },
                new SelectListItem { Value = "1", Text = "Admin" },
                new SelectListItem { Value = "2", Text = "Owner" },
                new SelectListItem { Value = "2", Text = "User" }
            };

            return list;
        }
    }
}
