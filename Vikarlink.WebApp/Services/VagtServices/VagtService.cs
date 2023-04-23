using System.Net.Http.Json;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.VagtServices
{
    public class VagtService : IVagtService
    {
        private readonly HttpClient _client;
        public VagtService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateVagtAsync(VagtRequestDto requestDto)
        {
            try
            {
                var result = await _client.PostAsJsonAsync("/VagtApi", requestDto);
                if (result.IsSuccessStatusCode)
                {
                    var respone = await result.Content.ReadFromJsonAsync<VagtRequestDto>();
                }
                else
                {
                    var message = await result.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {result.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }

            //var vagtDtoJson = new StringContent(
            //JsonSerializer.Serialize(requestDto),
            //Encoding.UTF8,
            //MediaTypeNames.Application.Json);
            //await _client.PostAsync("/VagtApi", vagtDtoJson);
        }

        public async Task DeleteVagtAsync(int Id)
        {
            try
            {
                var result = await _client.DeleteAsync($"/VagtApi/{Id}");
                if (result.IsSuccessStatusCode)
                {
                    var respone = await result.Content.ReadFromJsonAsync<VagtRequestDto>();
                }
                else
                {
                    var message = await result.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {result.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateVagtAsync(int Id, VagtQueryDto requestDto)
        {
            try
            {
                var result = await _client.PutAsJsonAsync($"/VagtApi/{Id}", requestDto);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadFromJsonAsync<VagtQueryDto>();
                }
                else
                {
                    var message = await result.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {result.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VagtQueryDto?> GetVagtAsync(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<VagtQueryDto?>($"/VagtApi/{Id}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VagtQueryDto>> GetVagterAsync()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<IEnumerable<VagtQueryDto>>("/VagtApi");
                if (result == null) throw new Exception();
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VagtQueryDto>> GetVikarVagterAsync(int vikarId)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<IEnumerable<VagtQueryDto>>($"/VikarApi/{vikarId}/Vagter");
                if (result == null) throw new Exception();
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
