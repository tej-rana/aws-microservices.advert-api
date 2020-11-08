using System;
using System.IO;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using AdvertApi.Management.Web.ServiceClients;
using AdvertApi.Management.Web.Services;
using AdvertApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Management.Web.Controllers
{
    public class AdvertsUploadController : Controller
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAdvertApiClient _advertApiClient;
        private readonly IMapper _mapper;

        public AdvertsUploadController(IFileUploader fileUploader, IAdvertApiClient advertApiClient, IMapper mapper)
        {
            _fileUploader = fileUploader;
            _advertApiClient = advertApiClient;
            _mapper = mapper;
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
                
                var createAdvertModel = _mapper.Map<CreateAdvertModel>(model);
                var response = await _advertApiClient.CreateAsync(createAdvertModel);
                var id = response.Id;
                
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

                        var confirmModel = new ConfirmAdvertRequest
                        {
                            Id = id,
                            FilePath = filePath,
                            Status = AdvertStatus.Active
                        };
                        var canConfirm = await _advertApiClient.ConfirmAsync(confirmModel);
                        if (!canConfirm)
                        {
                            throw new Exception($"Cannot confirm upload for advert {id}");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        var confirmModel = new ConfirmAdvertRequest
                        {
                            Id = id,
                            FilePath = filePath,
                            Status = AdvertStatus.Pending
                        };
                        await _advertApiClient.ConfirmAsync(confirmModel);
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return View(model);
        }
    }
}