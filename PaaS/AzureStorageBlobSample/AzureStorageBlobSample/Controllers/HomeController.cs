using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureStorageBlobSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageBlobSample.Controllers
{
    public class HomeController : Controller
    {
   
        private readonly IConfiguration  _configuration;
        public HomeController( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UploadFileModel uploadFileModel)
        {

            var blobContainerClient = new BlobContainerClient(connectionString: _configuration.GetConnectionString("BlobStorage"), "tetriscontainer");

           var blobClient= blobContainerClient.GetBlobClient($"{uploadFileModel.FileName}-{Guid.NewGuid()}");
            await blobClient.UploadAsync(uploadFileModel.File.OpenReadStream(),
                new BlobHttpHeaders
                {
                    ContentType = uploadFileModel.File.ContentType,
                    CacheControl="Public"
                }, new Dictionary<string, string> { { "CustomName", uploadFileModel.FileName } }
                );
           return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
