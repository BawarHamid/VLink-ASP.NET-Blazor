using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.VagtContract
{
    public interface IVagtRepository
    {
        Task CreateVagtAsync(Vagt vagt);
        Task<Vagt?> DeleteVagtAsync(int Id);
        Task EditVagtAsync(Vagt vagt, int Id);

        Task<Vagt?> GetVagtAsync(int Id);
        Task<IEnumerable<Vagt>> GetAllVagterAsync();

        int GetUserId();
        Task<IEnumerable<Vagt>> GetVikarensVagter(int vikarId);
    }
}
