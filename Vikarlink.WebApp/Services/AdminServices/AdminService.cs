using System.Net.Http.Json;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _client;
        public AdminService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateAdminAsync(AdminRequestDto requestDto)
        {
            try
            {
                var result = await _client.PostAsJsonAsync("/AdminApi", requestDto);
                if (result.IsSuccessStatusCode)
                {
                    var respone = await result.Content.ReadFromJsonAsync<AdminRequestDto>();
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

            //var adminDtoJson = new StringContent(
            //JsonSerializer.Serialize(requestDto),
            //Encoding.UTF8,
            //MediaTypeNames.Application.Json);
            //await _client.PostAsync("/AdminApi", adminDtoJson);
        }

        public async Task DeleteAdminAsync(int Id)
        {
            try
            {
                var result = await _client.DeleteAsync($"/AdminApi/{Id}");
                if (result.IsSuccessStatusCode)
                {
                    var respone = await result.Content.ReadFromJsonAsync<AdminRequestDto>();
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

        public async Task UpdateAdminAsync(int Id, AdminRequestDto requestDto)
        {
            try
            {
                var result = await _client.PutAsJsonAsync($"/AdminApi/{Id}", requestDto);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadFromJsonAsync<AdminRequestDto>();
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

        public async Task<AdminQueryDto?> GetAdminAsync(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<AdminQueryDto?>($"/AdminApi/{Id}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AdminQueryDto>> GetAdminsAsync()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<IEnumerable<AdminQueryDto>>("/AdminApi");
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
