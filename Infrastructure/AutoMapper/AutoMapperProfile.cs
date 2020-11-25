using AutoMapper;
using WebMerchantApi.Entities;
using WebMerchantApi.Models;
using WebMerchantApi.Models.Dto;

namespace WebMerchantApi.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BasketItem, BasketItemResponseDto>();
            CreateMap<BasketSummary, BasketSummaryResponseDto>();
        }
    }
}
