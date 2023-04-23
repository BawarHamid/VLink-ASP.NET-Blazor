using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.KlasseVaerelseContract
{
    public interface IKlasseVaerelseRepository
    {
        Task<KlasseVaerelse> GetKlasseVaerelseAsync(int KlasseVaerelseId);
        Task<IEnumerable<KlasseVaerelse>> GetAllKlasseVaerelserAsync();
    }
}
