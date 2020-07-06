using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Iemedebe.Domain
{
    public class FileToUpload
    {
        public IFormFile files { get; set; }
    }
}
