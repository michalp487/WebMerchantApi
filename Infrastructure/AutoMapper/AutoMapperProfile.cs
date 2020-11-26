using System;
using AutoMapper;
using Microsoft.OpenApi.Extensions;
using WebMerchantApi.Entities;
using WebMerchantApi.Enums;
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

            CreateMap<Order, OrderResponseDto>();

            CreateMap<OrderStatus, string>().ConvertUsing(src => src.ToString());
        }
    }
}
