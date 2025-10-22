using AutoMapper;
using Money.Domain.Models;
using Money.Application.ServiceDTO;

namespace Money.Api
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<CurrencyConversion, CurrencyConversionResponseDTO>().ReverseMap();

            //CreateMap<CurrencyConversion, CurrencyConversionResponseDTO>().ReverseMap();

            //CreateMap<CurrencyConversionRequestDTO, CurrencyConversion>()
            //    .ForMember(dest => dest.ConversionDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
