using Microsoft.AspNetCore.Mvc;
using SupabaseImageUpload.Model;

namespace SupabaseImageUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] CreateImageRequest request,
             [FromServices] Supabase.Client client)
        {
            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var bucket = client.Storage.From("photos");
            var fileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            await bucket.Upload(imageBytes, fileName);

            var publicUrl = bucket.GetPublicUrl(fileName);

            return Ok(new { Url = publicUrl });
        }
    }
}
