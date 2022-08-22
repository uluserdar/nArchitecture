using AutoMapper;
using RentACar.Application.Feaures.Brands.Commands.CreateBrand;
using RentACar.Application.Feaures.Brands.Dtos;
using RentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.someFeature.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Brand, CreatedBrandDto>().ReverseMap();
            CreateMap<Brand,CreateBrandCommand>().ReverseMap();
        }
    }
}
