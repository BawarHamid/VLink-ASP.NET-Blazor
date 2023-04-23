using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vikarlink.ApiInterface.Repositories.KlasseVaerelseContract;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Controllers
{
    [Route("/KlasseVaerelseApi")]
    [ApiController]
    public class KlasseVaerelseController : ControllerBase
    {
        private readonly IMapper _mapper;
        //public static List<KlasseVaerelse> KlasseVaerelseList = new List<KlasseVaerelse>();
        private readonly IKlasseVaerelseRepository _KlasseVaerelseRepo;

        public KlasseVaerelseController(IMapper mapper, IKlasseVaerelseRepository KlasseVaerelseRepo)
        {
            _mapper = mapper;
            _KlasseVaerelseRepo = KlasseVaerelseRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlasseVaerelseDto>>> GetAllKlasseVaerelserAsync()
        {
            try
            {
                var klasserToGet = await _KlasseVaerelseRepo.GetAllKlasseVaerelserAsync();
                if (klasserToGet is null)
                {
                    return NotFound("Ingen klasseværelser er registreret, tilføj en og prøv igen");
                }
                else
                {
                    return Ok(klasserToGet.Select(klasseToGet => _mapper.Map<KlasseVaerelseDto>(klasseToGet)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<KlasseVaerelseDto>> GetKlasseVaerelseAsync(int Id)
        {
            try
            {
                var klasseToGet = await _KlasseVaerelseRepo.GetKlasseVaerelseAsync(Id);
                if (klasseToGet is null)
                {
                    return NotFound("Ugyldigt id, klassen findes ikke");
                }
                else
                {
                    return Ok(_mapper.Map<KlasseVaerelseDto>(klasseToGet));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    
}
