using ManagerRestaurant.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagerRestaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFile : ControllerBase
    { 
        [HttpPost()]
        public async Task<Responsive> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var pathfiles = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var path = Directory.GetCurrentDirectory() + "\\wwwroot\\hinhanh\\";
                    if (!Directory.Exists(path))
                    {
                        //create if not exist
                        Directory.CreateDirectory(path);
                    }
                    var pathfile = DateTime.Now.Ticks + formFile.FileName;
                    var filePath = path + "\\" + pathfile;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    pathfiles.Add(@"/hinhanh/" +pathfile);
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return new Responsive(200, "Upload success", pathfiles);
        }
    }
}
