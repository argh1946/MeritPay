using AutoMapper;
using MeritPay.Core.DTOs;
using MeritPay.Core.Entities;

namespace MeritPay.WebApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Person, ReportDataVM>().ReverseMap();
        }
    }
}
