using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Repositories.AdminContract
{
    public class AdminRepository : IAdminRepository
    {
        private readonly VikarlinkContext _db;
        public AdminRepository(VikarlinkContext db)
        {
            this._db = db;
        }

        public async Task CreateAdminAsync(Admin admin)
        {
            try
            {
                _db.AdminDB.Add(admin);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Admin?> DeleteAdminAsync(int Id)
        {
            try
            {
                var admin = await _db.AdminDB.FirstOrDefaultAsync(x => x.Id == Id);
                if (admin is null) throw new Exception("Database error, administratoren kan ikke slettes");
                else
                {
                    _db.AdminDB.Remove(admin);
                    await _db.SaveChangesAsync();
                    return admin;
                }
            }
            catch (Exception)
            {
                throw;
            }

            //var admin = await _db.AdminDB.FindAsync(Id); - Virker
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

        public async Task EditAdminAsync(Admin request, int Id)
        {
            //var admin = await _db.AdminDB.FindAsync(Id);
            var admin = await _db.AdminDB.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                if (admin is null) throw new Exception("Database error, administratoren kan ikke opdateres");
                else
                {
                    admin.ForNavn = request.ForNavn;
                    admin.EfterNavn = request.EfterNavn;
                    admin.Telefon = request.Telefon;
                    admin.Email = request.Email;
                    admin.BrugerNavn = request.BrugerNavn;
                    admin.AdgangsKode = request.AdgangsKode;
                    admin.DatoOprettet = request.DatoOprettet;
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Admin?> GetAdminAsync(int adminID)
        {
            //return await _db.AdminDB.FindAsync(Id) ?? throw new Exception("Det indtastet id tilhører ingen admin, " +
            //    "prøv igen med et andet id");
            try
            {
                var query = _db.AdminDB.Where(x => x.Id == adminID);
                if (query is null) throw new Exception("Database error, administratoren findes ikke");
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

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            //var query = await _db.AdminDB.ToListAsync();
            //return query;
            try
            {
                var query = _db.AdminDB.OrderBy(x => x.Id);
                if (query is null) throw new Exception("Database error, ingen administratorer i databasen");
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
