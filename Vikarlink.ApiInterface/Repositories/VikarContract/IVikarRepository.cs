using Vikarlink.ApiInterface.Models;
using Vikarlink.Shared.Dtos.IncomingDtos;

namespace Vikarlink.ApiInterface.Repositories.VikarContract
{
    public interface IVikarRepository
    {
        Task CreateVikarAsync(VikarRequestDto vikar);
        Task DeleteVikarAsync(Vikar vikar);
        Task EditVikarAsync(Vikar vikar, int Id);

        Task<Vikar?> GetVikarAsync(int Id);
        Task<IEnumerable<Vikar>> GetAllVikarAsync();
        Task<Vikar> FindVikar(int vikarId);
        Vikar FindVikarByUsername(string username);

        //int GetUserId();
        //Task<Vikar?> HentVikarOplysninger();
    }
}
