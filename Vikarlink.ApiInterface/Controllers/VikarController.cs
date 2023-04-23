using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Models;
using Vikarlink.ApiInterface.Repositories.VagtContract;
using Vikarlink.ApiInterface.Repositories.VikarContract;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Controllers
{

    [Route("/VikarApi")]
    [ApiController]
    public class VikarController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVikarRepository _vikarRepo;
        private readonly IVagtRepository _vagtRepo;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public VikarController(IMapper mapper, IVikarRepository vikarRepo, RoleManager<IdentityRole> roleManager, UserManager<UserEntity> userManager, IVagtRepository vagtRepo)
        {
            _mapper = mapper;
            _vikarRepo = vikarRepo;
            _vagtRepo = vagtRepo;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewVikarAsync([FromBody] VikarRequestDto request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser is not null)
            {
                return BadRequest("Username is already taken");
            }

            if (await _roleManager.FindByNameAsync("User") is null)
            {
                await _roleManager.CreateAsync(new()
                {
                    Name = "User"
                });
            }

            var user = new UserEntity()
            {
                UserName = request.Username,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                await _vikarRepo.CreateVikarAsync(request);
                return Ok("User created");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VikarQueryDto>>> GetVikarerAsync()
        {
            try
            {
                var vikarerToGet = await _vikarRepo.GetAllVikarAsync();
                if (vikarerToGet is null)
                {
                    return NotFound("Ingen vikar er registreret, tilføj en og prøv igen");
                }
                else
                {
                    return Ok(vikarerToGet.Select(vikarGet => _mapper.Map<VikarQueryDto>(vikarGet)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //[HttpGet]
        //public async Task<ActionResult<VikarQueryDto>> GetVikarOplysninger()
        //{
        //    try
        //    {
        //        var vikarerToGet = await _vikarRepo.HentVikarOplysninger();
        //        if (vikarerToGet is null)
        //        {
        //            return NotFound("Ingen vikar er registreret, tilføj en og prøv igen");
        //        }
        //        else
        //        {
        //            //return Ok(vikarerToGet.Select(vikarGet => _mapper.Map<VikarQueryDto>(vikarGet)));
        //            return Ok(_mapper.Map<VikarQueryDto>(vikarerToGet));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        [HttpGet("{Id}")]
        public async Task<ActionResult<VikarQueryDto>> GetVikar(int Id)
        {
            try
            {
                var vikarToGet = await _vikarRepo.GetVikarAsync(Id);
                if (vikarToGet is null)
                {
                    return NotFound("Ugyldigt id, vikaren findes ikke");
                }
                else
                {
                    return Ok(_mapper.Map<VikarQueryDto>(vikarToGet));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{Id}/Vagter")]
        public async Task<ActionResult<IEnumerable<VagtQueryDto>>> GetVikarVagter(int Id)
        {
            //try
            //{
            //    var vikarToGet = await _vagtRepo.GetVikarensVagter(Id);
            //    if (vikarToGet is null)
            //    {
            //        return NotFound("Ugyldigt id, vikaren findes ikke");
            //    }
            //    else
            //    {
            //        return Ok(_mapper.Map<VikarQueryDto>(vikarToGet));
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            try
            {
                var vikarVagterToGet = await _vagtRepo.GetVikarensVagter(Id);
                //_vagtRepo.GetVikarensVagter();
                //Console.WriteLine("hiii");
                //return Ok(_vagtRepo.GetVikarensVagter());

                if (vikarVagterToGet is null)
                {
                    return NotFound("Ingen vagt er registreret, tilføj en og prøv igen");
                }
                else
                {
                    return Ok(vikarVagterToGet.Select(vagterGet => _mapper.Map<VagtQueryDto>(vagterGet)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateVikarAsync([FromBody] VikarQueryDto value, int Id)
        {
            try
            {
                var vikarToEdit = _mapper.Map<Vikar>(value);
                if (vikarToEdit is null)
                {
                    return NotFound("Ugyldigt id, vikaren kan ikke opdateres");
                }
                else
                {
                    await _vikarRepo.EditVikarAsync(vikarToEdit, Id);
                    return Ok(vikarToEdit);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVikarAsync(int vikarId)
        {
            var result = await _vikarRepo.FindVikar(vikarId);
            if (result != null)
            {
                var existingUser = await _userManager.FindByNameAsync(result.Username);
                if (existingUser is not null)
                {
                    await _userManager.DeleteAsync(existingUser);
                }
                await _vikarRepo.DeleteVikarAsync(result);
                return Ok();
            }
            return NotFound();
        }
    }
}
