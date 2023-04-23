using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Models;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Repositories.VikarContract
{
    public class VikarRepository : IVikarRepository
    {
        private readonly VikarlinkContext _db;
        private readonly IHttpContextAccessor _httpCA;

        public VikarRepository(VikarlinkContext db, IHttpContextAccessor httpCA)
        {
            this._db = db;
            this._httpCA = httpCA;
        }

        public async Task CreateVikarAsync(VikarRequestDto vikar)
        {

            Vikar nyVikar = new()
            {

                ForNavn = vikar.ForNavn,
                EfterNavn = vikar.EfterNavn,
                Adresse = vikar.Adresse,
                Telefon = vikar.Telefon,
                Email = vikar.Email,
                Username = vikar.Username,
                Password = vikar.Password,
                DatoOprettet = vikar.DatoOprettet,
                AnsaettelsesDato = vikar.AnsaettelsesDato,
                OpsigelsesDato = vikar.OpsigelsesDato,

            };
            await _db.AddAsync(nyVikar);
            await _db.SaveChangesAsync();



        }

        public async Task DeleteVikarAsync(Vikar vikar)
        {

            _db.VikarDB.Remove(vikar);
            await _db.SaveChangesAsync();
        }

        public async Task EditVikarAsync(Vikar request, int Id)
        {
            //var vikar = await _db.VikarDB.FindAsync(Id);
            var vikar = await _db.VikarDB.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                if (vikar is null) throw new Exception("Database error, Vikaren kan ikke opdateres");
                else
                {
                    vikar.ForNavn = request.ForNavn;
                    vikar.EfterNavn = request.EfterNavn;
                    vikar.Adresse = request.Adresse;
                    vikar.Telefon = request.Telefon;
                    vikar.Email = request.Email;
                    vikar.Username = request.Username;
                    vikar.Password = request.Password;
                    //vikar.DatoOprettet = request.DatoOprettet;
                    //vikar.AnsaettelsesDato = request.AnsaettelsesDato;
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Vikar?> GetVikarAsync(int Id)
        {
            //return await _db.VikarDB.FindAsync(Id) ?? throw new Exception("Det indtastet id tilhører ingen vikar, " +
            //    "prøv igen med et andet id");

            try
            {
                var query = _db.VikarDB.Where(x => x.Id == Id);
                if (query is null) throw new Exception("Database error, vikaren findes ikke");
                else
                {
                    return await query.FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Vikar>> GetAllVikarAsync()
        {
            try
            {
                var query = _db.VikarDB.OrderBy(x => x.Id);
                if (query is null) throw new Exception("Database error, ingen vikarer i databasen");
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            //var query = _db.VikarDB.OrderBy(x => x.ForNavn);
            //return await query.ToArrayAsync();
        }

        public async Task<Vikar> FindVikar(int vikarId)
        {
            if (vikarId != null)
            {
                var result = await _db.FindAsync<Vikar>(vikarId);
                return result;
            }
            return null;
        }

        public Vikar FindVikarByUsername(string username)
        {
            var result = _db.VikarDB.FirstOrDefault(searchVikar => searchVikar.Username == username);
            return result != null ? result : new Vikar();
            //return await _db.VikarDB.FirstOrDefaultAsync
        }

        //public int GetUserId() => int.Parse(_httpCA.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        //public async Task<Vikar?> HentVikarOplysninger()
        //{
        //    try
        //    {
        //        var query = _db.VikarDB.Where(v => v.Id == GetUserId());
        //        if (query is null) throw new Exception("Database error, ingen vagter i databasen");
        //        else
        //        {
        //            return await query.FirstOrDefaultAsync();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}