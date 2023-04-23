using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.VagtServices
{
    public interface IVagtService
    {
        Task CreateVagtAsync(VagtRequestDto vagtDto);
        Task DeleteVagtAsync(int Id);
        Task UpdateVagtAsync(int Id, VagtQueryDto vagtDto);
        Task<VagtQueryDto?> GetVagtAsync(int Id);
        Task<IEnumerable<VagtQueryDto>> GetVagterAsync();
        Task<IEnumerable<VagtQueryDto>> GetVikarVagterAsync(int vikarId);
    }
}
