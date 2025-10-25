using AutoMapper;
using Money.Application.ServiceDTO;
using Money.Domain.Models;

namespace Money.Api
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<CurrencyConversion, CurrencyConversionResponseDTO>().ReverseMap();

            //CreateMap<CurrencyConversionRequestDTO, CurrencyConversion>()
            //    .ForMember(dest => dest.ConversionDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
