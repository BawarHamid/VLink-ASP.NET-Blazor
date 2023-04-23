using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.AdminServices
{
    public interface IAdminService
    {
        Task CreateAdminAsync(AdminRequestDto adminDto);
        Task DeleteAdminAsync(int Id);
        Task UpdateAdminAsync(int Id, AdminRequestDto adminDto);
        Task<AdminQueryDto?> GetAdminAsync(int Id);
        Task<IEnumerable<AdminQueryDto>> GetAdminsAsync();
    }
}
