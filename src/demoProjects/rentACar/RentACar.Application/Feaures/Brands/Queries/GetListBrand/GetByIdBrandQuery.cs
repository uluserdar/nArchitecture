﻿using AutoMapper;
using MediatR;
using RentACar.Application.Feaures.Brands.Dtos;
using RentACar.Application.Feaures.Brands.Rules;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Feaures.Brands.Queries.GetListBrand
{
    public class GetByIdBrandQuery : IRequest<BrandGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRule _brandBusinessRule;

            public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRule brandBusinessRule)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRule = brandBusinessRule;
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                Brand brand = await _brandRepository.GetAsync(b => b.Id == request.Id);
                _brandBusinessRule.BrandShouldExistsWhenRequested(brand);
                BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);
                return brandGetByIdDto;
            }
        }
    }
}
