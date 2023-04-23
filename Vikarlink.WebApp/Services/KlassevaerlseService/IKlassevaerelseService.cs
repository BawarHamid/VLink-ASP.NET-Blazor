using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.WebApp.Services.KlassevaerlseService
{
    public interface IKlassevaerelseService
    {
        Task<KlasseVaerelseDto?> GetKlasseVaerelseAsync(int Id);
        Task<IEnumerable<KlasseVaerelseDto>> GetKlasseVaerelserAsync();
    }
}
