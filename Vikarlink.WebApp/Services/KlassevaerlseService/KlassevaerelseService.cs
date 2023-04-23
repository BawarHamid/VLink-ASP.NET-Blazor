using System.Net.Http.Json;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.KlassevaerlseService
{
    public class KlassevaerelseService : IKlassevaerelseService
    {
        private readonly HttpClient _client;
        public KlassevaerelseService(HttpClient client)
        {
            _client = client;
        }

        public async Task<KlasseVaerelseDto?> GetKlasseVaerelseAsync(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<KlasseVaerelseDto?>($"/KlasseVaerelseApi/{Id}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<KlasseVaerelseDto>> GetKlasseVaerelserAsync()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<IEnumerable<KlasseVaerelseDto>>("/KlasseVaerelseApi");
                if (result == null)
                {
                    throw new Exception("Http status 404, Ingen klassevaerelse i databasen");
                }
                else
                {
                    return result;
                }
                //return await _client.GetFromJsonAsync<IEnumerable<VikarQueryDto>>($"/VikarApi");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
