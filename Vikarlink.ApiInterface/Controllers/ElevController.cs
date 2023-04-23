using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vikarlink.ApiInterface.Models;
using Vikarlink.ApiInterface.Repositories.ElevContract;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Controllers
{
    [Route("/ElevApi")]
    [ApiController]
    public class ElevController : ControllerBase
    {

        private readonly IMapper _mapper;

        //public static List<Elev> ElevList = new List<Elev>();
        private readonly IElevRepository _elevRepo;

        public ElevController(IMapper mapper, IElevRepository elevRepo)
        {
            _mapper = mapper;
            _elevRepo = elevRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElevDto>>> GetEleverAsync()
        {
            try
            {
                var elev = await _elevRepo.GetAllEleverAsync();

                if (elev == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elev.Select(elev => _mapper.Map<ElevDto>(elev)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ElevDto>> GetElev(int Id)
        {
            try
            {
                var elev = await _elevRepo.GetElevAsync(Id);

                if (elev == null)
                {
                    return BadRequest("Vikaren findes ikke");
                }
                else
                {
                    return Ok(_mapper.Map<ElevDto>(elev));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewElevAsync([FromBody] ElevCreateDto dto)
        {
            try
            {
                var elev = _mapper.Map<Elev>(dto);
                await _elevRepo.CreateElevAsync(elev);

                return Ok(elev);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateElevAsync([FromBody] ElevCreateDto dto, int Id)
        {
            try
            {
                var elev = _mapper.Map<Elev>(dto);
                await _elevRepo.EditElevAsync(elev, Id);

                return Ok(elev);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ElevDto>> DeleteElevAsync(int Id)
        {
            try
            {
                var elev = await _elevRepo.DeleteElevAsync(Id);
                if (elev == null)
                {
                    return BadRequest("Eleven findes ikke");
                }
                else
                {
                    return Ok(_mapper.Map<ElevDto>(elev));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
