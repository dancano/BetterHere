using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Helpers
{
    public interface IBlobHelper
    {
        Task<string> UploadBlobAsync(IFormFile file, string containerName);

        Task<string> UploadBlobAsync(byte[] file, string containerName);

        Task<string> UploadBlobAsync(string image, string containerName);
    }

}
