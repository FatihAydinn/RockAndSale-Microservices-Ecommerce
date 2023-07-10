using AutoMapper;
using RAS.Services.BagAPI.Models;
using RAS.Services.BagAPI.Models.Dto;

namespace RAS.Services.BagAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<BagHeader, BagHeaderDto>().ReverseMap();
                config.CreateMap<BagDetails, BagDetailsDto>().ReverseMap();
                config.CreateMap<Bag, BagDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
