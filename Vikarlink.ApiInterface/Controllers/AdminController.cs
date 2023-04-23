using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vikarlink.ApiInterface.Models;
using Vikarlink.ApiInterface.Repositories.AdminContract;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Controllers
{
    [Route("/AdminApi")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepo;

        public AdminController(IMapper mapper, IAdminRepository adminRepo)
        {
            _mapper = mapper;
            _adminRepo = adminRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminQueryDto>>> GetAdminsAsync()
        {
            try
            {
                var adminsToGet = await _adminRepo.GetAllAdminsAsync();
                if (adminsToGet is null)
                {
                    return NotFound("Ingen administrator er registreret, tilføj en og prøv igen");
                }
                else
                {
                    return Ok(adminsToGet.Select(adminGet => _mapper.Map<AdminQueryDto>(adminGet)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AdminQueryDto>> GetAdmin(int Id)
        {
            try
            {
                var adminToGet = await _adminRepo.GetAdminAsync(Id);
                if (adminToGet is null)
                {
                    return NotFound("Ugyldigt id, administratoren findes ikke");
                }
                else
                {
                    return Ok(_mapper.Map<AdminQueryDto>(adminToGet));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminAsync([FromBody] AdminRequestDto value)
        {
            try
            {
                var adminToAdd = _mapper.Map<Admin>(value);
                await _adminRepo.CreateAdminAsync(adminToAdd);
                return Ok(adminToAdd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] AdminRequestDto value, int Id)
        {
            try
            {
                var adminToEdit = _mapper.Map<Admin>(value);
                if (adminToEdit is null)
                {
                    return NotFound("Ugyldigt id, administratoren kan ikke opdateres");
                }
                else
                {
                    await _adminRepo.EditAdminAsync(adminToEdit, Id);
                    return Ok(adminToEdit);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Admin>> DeleteAdminAsync(int Id)
        {
            try
            {
                var adminToRemove = await _adminRepo.DeleteAdminAsync(Id);
                if (adminToRemove is null)
                {
                    return NotFound("Ugyldigt id, administratoren kan ikke slettes");
                }
                else
                {
                    return Ok(_mapper.Map<AdminQueryDto>(adminToRemove));
                }

                //await _vikarRepo.DeleteVikarAsync(Id);
                //return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
