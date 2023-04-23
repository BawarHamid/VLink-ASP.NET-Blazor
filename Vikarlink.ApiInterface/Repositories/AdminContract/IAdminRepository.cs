using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.AdminContract
{
    public interface IAdminRepository
    {
        Task CreateAdminAsync(Admin admin);
        Task<Admin?> DeleteAdminAsync(int Id);
        Task EditAdminAsync(Admin admin, int Id);

        Task<Admin?> GetAdminAsync(int Id);
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
    }
}
