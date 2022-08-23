using AutoMapper;
using StandingOrders.API.Entities;
using StandingOrders.API.Models;
using StandingOrders.API.Models.Dto;
using StandingOrders.API.Models.Entities;

namespace StandingOrders.API.Profiles
{
    public class StandingOrdersProfile : Profile
    {
        public StandingOrdersProfile()
        {
            CreateMap<StandingOrder, StandingOrderBrowseDto>()
                .ForMember(x => x.Interval, opt => opt.MapFrom(src => src.Interval.Value))
                .AfterMap((src, dest) => dest.NextRealizationDate = dest.CalculateNextRealizationDate());

            CreateMap<StandingOrder, StandingOrderDetailDto>();

            CreateMap<Interval, CodeTableDto<int>>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.IntervalId))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Value));

            CreateMap<ConstantSymbol, CodeTableDto<string>>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.ConstantSymbolValue))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Description));

            CreateMap<StandingOrderDetailDto, StandingOrder>()
                .ForMember(x => x.StandingOrderId, opt => opt.Ignore());
        }
    }
}