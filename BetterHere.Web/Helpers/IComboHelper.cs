using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BetterHere.Web.Helpers
{
    public interface IComboHelper
    {
        IEnumerable<SelectListItem> GetComboRoles();
    }
}
