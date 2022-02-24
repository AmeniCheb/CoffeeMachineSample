using AutoMapper;
using Store.ApplicationCore.DTOs;
using Store.ApplicationCore.Entities;

namespace Store.ApplicationCore.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateDrinkRequest, Drink>();
            CreateMap<Drink, DrinkResponse>();
        }
    }
}
