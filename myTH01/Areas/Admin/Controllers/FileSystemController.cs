using elFinder.NetCore.Drivers.FileSystem;
using elFinder.NetCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace myTH01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/el-finder-file-system")]
    public class FileSystemController : Controller
    {
        readonly IWebHostEnvironment _env;
        public FileSystemController(IWebHostEnvironment env) => _env = env;
        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            var result = await connector.ProcessAsync(Request);
            if (result is JsonResult)
            {
                var json = result as JsonResult;
                return Content(JsonSerializer.Serialize(json.Value), json.ContentType);
            }
            else
            {
                return Json(result);
            }
        }
        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }
        private Connector GetConnector()
        {
            string pathroot = "files";

            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            string rootDirectory = Path.Combine(_env.WebRootPath, pathroot);

            string url = $"/{pathroot}/";
            string urlthumb = $"{uri.Scheme}://Admin/el-finder-file-system/thumb/";

            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                IsReadOnly = false,
                IsLocked = false,
                Alias = "Files",
                ThumbnailSize = 100,
            };

            driver.AddRoot(root);

            return new Connector(driver)
            {
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
