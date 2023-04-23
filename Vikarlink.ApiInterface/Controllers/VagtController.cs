using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vikarlink.ApiInterface.Models;
using Vikarlink.ApiInterface.Repositories.VagtContract;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Controllers
{
    [Route("/VagtApi")]
    [ApiController]
    public class VagtController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVagtRepository _vagtRepo;

        public VagtController(IMapper mapper, IVagtRepository vagtRepo)
        {
            _mapper = mapper;
            _vagtRepo = vagtRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VagtQueryDto>>> GetVagterAsync()
        {
            try
            {
                var vagterToGet = await _vagtRepo.GetAllVagterAsync();

                if (vagterToGet is null)
                {
                    return NotFound("Ingen vagt er registreret, tilføj en og prøv igen");
                }
                else
                {
                    return Ok(vagterToGet.Select(vagterGet => _mapper.Map<VagtQueryDto>(vagterGet)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet("{vikarId}")]
        //public async Task<ActionResult<IEnumerable<VagtQueryDto>>> GetVikarensVagterAsync(int vikarId)
        //{
        //    try
        //    {
        //        var vikarVagterToGet = await _vagtRepo.GetVikarensVagter(vikarId);

        //        if (vikarVagterToGet is null)
        //        {
        //            return NotFound("Ingen vagt er registreret, tilføj en og prøv igen");
        //        }
        //        else
        //        {
        //            return Ok(vikarVagterToGet.Select(vagterGet => _mapper.Map<VagtQueryDto>(vagterGet)));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpGet("{Id}")]
        public async Task<ActionResult<AdminQueryDto>> GetVagt(int Id)
        {
            try
            {
                var vagtToGet = await _vagtRepo.GetVagtAsync(Id);

                if (vagtToGet is null)
                {
                    return NotFound("Ugyldigt id, vagten findes ikke");
                }
                else
                {
                    return Ok(_mapper.Map<VagtQueryDto>(vagtToGet));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVagtAsync([FromBody] VagtRequestDto value)
        {
            try
            {
                var vagtToAdd = _mapper.Map<Vagt>(value);
                await _vagtRepo.CreateVagtAsync(vagtToAdd);

                return Ok(vagtToAdd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateVagtAsync([FromBody] VagtQueryDto value, int Id)
        {
            try
            {
                var vagtToEdit = _mapper.Map<Vagt>(value);
                if (vagtToEdit is null)
                {
                    return NotFound("Ugyldigt id, vagten kan ikke opdateres");
                }
                else
                {
                    await _vagtRepo.EditVagtAsync(vagtToEdit, Id);
                    return Ok(vagtToEdit);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Vagt>> DeleteVagtAsync(int Id)
        {
            try
            {
                var vagtToRemove = await _vagtRepo.DeleteVagtAsync(Id);
                if (vagtToRemove is null)
                {
                    return NotFound("Ugyldigt id, vagten kan ikke slettes");
                }
                else
                {
                    return Ok(_mapper.Map<VagtQueryDto>(vagtToRemove));
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
