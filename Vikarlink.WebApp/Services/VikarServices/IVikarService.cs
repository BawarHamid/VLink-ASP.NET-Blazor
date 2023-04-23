using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.VikarServices
{
    public interface IVikarService
    {
        Task CreateVikarAsync(VikarRequestDto vikarDto);
        Task DeleteVikarAsync(int Id);
        Task UpdateVikarAsync(int Id, VikarQueryDto vikarDto);
        Task<VikarQueryDto?> GetVikarAsync(int Id);
        Task<IEnumerable<VikarQueryDto>> GetVikareAsync();
        //Task<VikarQueryDto?> HentVikarOp();
    }
}
