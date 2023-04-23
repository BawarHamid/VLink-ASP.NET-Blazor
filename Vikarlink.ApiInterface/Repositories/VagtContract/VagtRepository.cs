using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Models;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Repositories.VagtContract
{
    public class VagtRepository : IVagtRepository
    {
        private readonly VikarlinkContext _db;
        private readonly IHttpContextAccessor _httpCA;
        public VagtRepository(VikarlinkContext db, IHttpContextAccessor httpCA)
        {
            _db = db;
            _httpCA = httpCA;
        }

        public async Task CreateVagtAsync(Vagt vagt)
        {
            try
            {
                _db.VagtDB.Add(vagt);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Vagt?> DeleteVagtAsync(int Id)
        {
            try
            {
                var vagt = await _db.VagtDB.FirstOrDefaultAsync(x => x.Id == Id);
                if (vagt == null) throw new Exception("Database error, vagten kan ikke slettes");
                else
                {
                    _db.VagtDB.Remove(vagt);
                    await _db.SaveChangesAsync();
                    return vagt;
                }
            }
            catch (Exception)
            {
                throw;
            }
            //var admin = await _db.AdminDB.FindAsync(Id); - Virker også
            //if (admin != null)
            //{
            //    _db.AdminDB.Remove(admin);
            //    await _db.SaveChangesAsync();
            //}
            //return admin;

            //var vikar = _db.VikarDB.FindAsync(vikarID);
            //if (vikar is null) return null;
            //_db.VikarDB.Remove(vikar);
            //await _db.SaveChangesAsync();
        }

        public async Task EditVagtAsync(Vagt request, int Id)
        {
            try
            {
                //var vagt = await _db.VagtDB.FindAsync(Id);
                var vagt = await _db.VagtDB.FirstOrDefaultAsync(x => x.Id == Id);
                if (vagt is null) throw new Exception("Database error, vagten kan ikke opdateres");
                else
                {
                    vagt.Dato = request.Dato;
                    vagt.StartTid = request.StartTid;
                    vagt.SlutTid = request.SlutTid;
                    vagt.Status = request.Status;
                    vagt.VikarId = request.VikarId;
                    vagt.KlasseVaerelseId = request.KlasseVaerelseId;
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Vagt?> GetVagtAsync(int vagtID)
        {
            //return await _db.AdminDB.FindAsync(Id) ?? throw new Exception("Det indtastet id tilhører ingen admin, " +
            //    "prøv igen med et andet id");
            try
            {
                var query = _db.VagtDB.Where(x => x.Id == vagtID);
                if (query is null) throw new Exception("Database error, vagten findes ikke");
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

        public async Task<IEnumerable<Vagt>> GetAllVagterAsync()
        {
            //var query = await _db.AdminDB.ToListAsync();
            //return query;
            try
            {
                var query = _db.VagtDB.OrderBy(x => x.Id);
                if (query is null) throw new Exception("Database error, ingen vagter i databasen");
                else
                {
                    //return await query.ToArrayAsync();
                    return await query.ToListAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public int GetUserId() => int.Parse(_httpCA.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public int GetUserId()
        {
            var userIdString = _httpCA.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
            {
                Console.WriteLine("hi");
                return 2;
            }

            return int.Parse(userIdString);
        }

        public async Task<IEnumerable<Vagt>> GetVikarensVagter(int vikarId)
        {
            try
            {
                var query = _db.VagtDB.Where(v => v.VikarId == vikarId);
                if (query is null) throw new Exception("Database error, ingen vagter i databasen");
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
