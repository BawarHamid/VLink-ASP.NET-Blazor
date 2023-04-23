using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.ElevContract
{
    public interface IElevRepository
    {
        Task CreateElevAsync(Elev elev);
        Task<Elev?> DeleteElevAsync(int Id);
        Task EditElevAsync(Elev elev, int ElevId);

        Task<Elev> GetElevAsync(int Id);
        Task<IEnumerable<Elev>> GetAllEleverAsync();
    }
}
