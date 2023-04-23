using AutoMapper;
using Vikarlink.ApiInterface.Models;
using Vikarlink.Shared.Dtos.IncomingDtos;
using Vikarlink.Shared.Dtos.OutgoingDtos;

namespace Vikarlink.ApiInterface.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            
            CreateMap<VikarRequestDto, Vikar>().ReverseMap();
            CreateMap<VikarQueryDto, Vikar>().ReverseMap();
            CreateMap<AdminRequestDto, Admin>().ReverseMap();
            CreateMap<AdminQueryDto, Admin>().ReverseMap();
            CreateMap<VagtRequestDto, Vagt>().ReverseMap();
            CreateMap<VagtQueryDto, Vagt>().ReverseMap();

            CreateMap<ElevCreateDto, Elev>().ReverseMap();
            CreateMap<ElevDto, Elev>().ReverseMap();
            
            CreateMap<KlasseVaerelseDto, KlasseVaerelse>().ReverseMap();
        }
    }
}
