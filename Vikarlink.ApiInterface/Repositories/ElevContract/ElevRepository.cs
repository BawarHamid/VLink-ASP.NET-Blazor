using Microsoft.EntityFrameworkCore;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.ElevContract
{
    public class ElevRepository : IElevRepository
    {
        private readonly VikarlinkContext _db;

        public ElevRepository(VikarlinkContext db)
        {
            this._db = db;
        }

        public async Task CreateElevAsync(Elev elev)
        {
            _db.Add(elev);
            await _db.SaveChangesAsync();
        }

        public async Task<Elev?> DeleteElevAsync(int elevId)
        {
            var elev = await _db.ElevDB.FindAsync(elevId);

            if (elev != null)
            {
                _db.ElevDB.Remove(elev);
                await _db.SaveChangesAsync();
            }
            return elev;
        }

        public async Task EditElevAsync(Elev elevRequest, int elevId)
        {

            var elev = await _db.ElevDB.FindAsync(elevId);

            if (elev != null)
            {
                //elev.Id = elevRequest.Id;
                elev.CprNr = elevRequest.CprNr;
                elev.ForNavn = elevRequest.ForNavn;
                elev.EfterNavn = elevRequest.EfterNavn;
                elev.Adresse = elevRequest.Adresse;
                elev.Mobil = elevRequest.Mobil;
                elev.KontaktNr = elevRequest.KontaktNr;
                elev.Email = elevRequest.Email;
                elev.DatoOprettet = elevRequest.DatoOprettet;
                elev.FotoURL = elevRequest.FotoURL;
                elev.KlasseVaerelseId = elevRequest.KlasseVaerelseId;
                await _db.SaveChangesAsync();
            }
            else
            {

            }
        }

        public async Task<Elev> GetElevAsync(int elevId)
        {

            var query = _db.ElevDB.Where(x => x.Id == elevId);

            return await query.FirstOrDefaultAsync() ?? throw new Exception("Det indtastede id tilhører ingen elever, " +
             "prøv igen med et andet id");
        }

        

        public async Task<IEnumerable<Elev>> GetAllEleverAsync()
        {
            var query = await _db.ElevDB.ToListAsync();

            return query;
        }
    }
}
