using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
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

        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string constFileName = "asdfasd-af17661c-7e07-4977-b543-84ea66b0d2f9";
            var blobContainerClient = new BlobContainerClient(connectionString: _configuration.GetConnectionString("BlobStorage"), "tetriscontainer");
            var blobClient = blobContainerClient.GetBlobClient(constFileName);

            BlobSasBuilder blobSasBuilder = new BlobSasBuilder
            {
                BlobContainerName = blobContainerClient.Name,
                BlobName = blobClient.Name,
                ExpiresOn = DateTime.Now.AddMinutes(2),
                Protocol = SasProtocol.Https
            };
            blobSasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

            UriBuilder uriBuilder = new UriBuilder(blobClient.Uri);
            uriBuilder.Query = blobSasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobContainerClient.AccountName, "account-key")).ToString();
            var url = uriBuilder.Uri.ToString();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UploadFileModel uploadFileModel)
        {

            var blobContainerClient = new BlobContainerClient(connectionString: _configuration.GetConnectionString("BlobStorage"), "tetriscontainer");

            var blobClient = blobContainerClient.GetBlobClient($"{uploadFileModel.FileName}-{Guid.NewGuid()}");
            await blobClient.UploadAsync(uploadFileModel.File.OpenReadStream(),
                new BlobHttpHeaders
                {
                    ContentType = uploadFileModel.File.ContentType,
                    CacheControl = "Public"
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
