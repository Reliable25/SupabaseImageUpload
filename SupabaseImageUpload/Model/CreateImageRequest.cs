namespace SupabaseImageUpload.Model
{
    public class CreateImageRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
