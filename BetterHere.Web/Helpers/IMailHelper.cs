using BetterHere.Common.Models;

namespace BetterHere.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
