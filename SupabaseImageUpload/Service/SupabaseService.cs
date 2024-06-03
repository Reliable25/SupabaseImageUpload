using Supabase;

namespace SupabaseImageUpload.Service
{
    public class SupabaseService
    {
        private readonly Client _client;

        public SupabaseService(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:Key"];
            _client = new Client(url, key);
        }

        public Client GetClient() => _client;
    }
}
