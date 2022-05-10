using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WEB
{
    public class ImageHandler
    {
        public async Task<string> UploadFile(IWebHostEnvironment _env, IFormFile formFile, string folderName = "Images")
        {
            if (formFile != null)
            {
                try
                {
                    var uniqueFileName = GetUniqueFileName(formFile.FileName);
                    var dir = Path.Combine(_env.WebRootPath, folderName);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var filePath = Path.Combine(dir, uniqueFileName);
                    await formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    return uniqueFileName;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Guid.NewGuid().ToString().Substring(0, 15)
                   + Path.GetExtension(fileName);
        }
    }
}
