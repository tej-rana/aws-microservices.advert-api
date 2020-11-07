using System;
using System.IO;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using AdvertApi.Management.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Management.Web.Controllers
{
    public class AdvertsUploadController : Controller
    {
        private readonly IFileUploader _fileUploader;

        public AdvertsUploadController(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        public IActionResult Create(CreateAdvertViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var id = "11111"; 
                // Todo: Make a call to advert api to create advertisement in the database and return the id
                var fileName = "";
                if (imageFile != null)
                {
                    fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
                    var filePath = $"{id}/{fileName}";

                    try
                    {
                        using (var readStream = imageFile.OpenReadStream())
                        {
                            var result = await _fileUploader.UploadFileAsync(filePath, readStream);
                            if (!result)
                                throw new Exception(
                                    " Could not upload image to the file repository. Please see logs for more detail");
                        }

                        //TODO: Call Advert APi and confirm the advertisement

                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        //TODO: Call Advert Api and cancel the advertisement
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return View(model);
        }
    }
}