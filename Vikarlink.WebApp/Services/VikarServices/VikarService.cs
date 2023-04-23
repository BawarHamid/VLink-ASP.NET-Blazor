using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.VikarServices
{
    public class VikarService : IVikarService
    {
        private readonly HttpClient _client;

        public VikarService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateVikarAsync(VikarRequestDto requestDto)
        {
            try
            {
                var result = await _client.PostAsJsonAsync("/VikarApi", requestDto);

                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<VikarRequestDto>();
                }
                else
                {
                    var message = await result.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {result.StatusCode} Message - {message}");
                }

                //var vikarDtoJson = new StringContent(
                //JsonSerializer.Serialize(requestDto),
                //Encoding.UTF8,
                //MediaTypeNames.Application.Json);
                //await _client.PostAsync("/VikarApi", vikarDtoJson);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteVikarAsync(int Id)
        {
            try
            {
                var result = await _client.DeleteAsync($"/VikarApi/{Id}");
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<VikarRequestDto>();
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

        public async Task UpdateVikarAsync(int Id, VikarQueryDto dto)
        {
            try
            {
                var result = await _client.PutAsJsonAsync($"/VikarApi/{Id}", dto);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<VikarQueryDto>();
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

        public async Task<VikarQueryDto?> GetVikarAsync(int Id)
        {
            try
            {
                return await _client.GetFromJsonAsync<VikarQueryDto?>($"/VikarApi/{Id}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<VikarQueryDto>> GetVikareAsync()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<IEnumerable<VikarQueryDto>>("/VikarApi");
                if (result == null)
                {
                    throw new Exception("Http status 404, Ingen vikarer i databasen");
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


        //public async Task<VikarQueryDto?> HentVikarOp()
        //{
        //    try
        //    {
        //        var result = await _client.GetFromJsonAsync<VikarQueryDto>("/VikarApi");
        //        if (result == null)
        //        {
        //            throw new Exception("Http status 404, Ingen vikarer i databasen");
        //        }
        //        else
        //        {
        //            return result;
        //        }

        //        //return await _client.GetFromJsonAsync<IEnumerable<VikarQueryDto>>($"/VikarApi");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}