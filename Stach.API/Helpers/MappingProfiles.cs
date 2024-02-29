using AutoMapper;
using Stach.API.DTOs;
using Stach.Domain.Models;

namespace Stach.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasket, CustomerBasketDTO>();
            CreateMap<BasketItem, BasketItemDTO>();
        }
    }
}
