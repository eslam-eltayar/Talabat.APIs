using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(P => P.Brand, o => o.MapFrom(S => S.Brand.Name))
                .ForMember(P=> P.Category, o=> o.MapFrom(S=>S.Category.Name))
                .ForMember(P => P.PictureUrl, o=> o.MapFrom<ProductPictureUrlResolver>());


            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
