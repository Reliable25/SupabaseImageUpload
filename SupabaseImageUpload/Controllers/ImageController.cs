using Microsoft.AspNetCore.Mvc;
using SupabaseImageUpload.Model;
using SupabaseImageUpload.Service;

namespace SupabaseImageUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly SupabaseService _supabaseService;

        public ImageController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] CreateImageRequest request)
        {
            var client = _supabaseService.GetClient();
            var storage = client.Storage;

            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var bucket = storage.From("photos");
            var fileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            await bucket.Upload(imageBytes, fileName);

            var publicUrl = bucket.GetPublicUrl(fileName);

            return Ok(new { Url = publicUrl });
        }
    }
}
