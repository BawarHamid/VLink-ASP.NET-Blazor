using Microsoft.EntityFrameworkCore;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.KlasseVaerelseContract
{
    public class KlasseVaerelseRepository : IKlasseVaerelseRepository
    {
        private readonly VikarlinkContext _db;

        public KlasseVaerelseRepository(VikarlinkContext db)
        {
            this._db = db;
        }

        public async Task<KlasseVaerelse> GetKlasseVaerelseAsync(int KlasseVaerelseId)
        {
           var query = _db.KlassevaerelseDB.Where(x => x.Id == KlasseVaerelseId);

            return await query.FirstOrDefaultAsync() ?? throw new Exception("Det indtastede id tilhører ingen klasse værelser, " +
             "prøv igen med et andet id");
        }



        public async Task<IEnumerable<KlasseVaerelse>> GetAllKlasseVaerelserAsync()
        {
            var query = await _db.KlassevaerelseDB.ToListAsync();

            return query;
        }
    }
}

