using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            using var ms = new MemoryStream();

            await file.CopyToAsync(ms);

            var buff = ms.ToArray();

            var hash = MD5.HashData(buff);

            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i += 4)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            if (!Directory.Exists("wwwroot/images"))
            {
                Directory.CreateDirectory("wwwroot/images");
            }

            var fileName = $"images/{sb}.{file.FileName.Split('.').Last()}";

            using var fs = new FileStream($"wwwroot/{fileName}", FileMode.OpenOrCreate, FileAccess.Write);

            await fs.WriteAsync(buff);

            var url = $"/{fileName}";

            return Created(url, new
            {
                url
            });
        }
    }
}
