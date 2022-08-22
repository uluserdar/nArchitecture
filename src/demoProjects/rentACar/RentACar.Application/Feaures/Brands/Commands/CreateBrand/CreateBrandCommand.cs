using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RentACar.Application.Feaures.Brands.Dtos;
using RentACar.Application.Feaures.Brands.Rules;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Feaures.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand: IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRule _brandBusinessRule;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRule brandBusinessRule)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRule = brandBusinessRule;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRule.BrandNameCanNotBeDublicatedWhenInserted(request.Name);

                Brand mappedBrand = _mapper.Map<Brand>(request);
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);
                return createdBrandDto;
            }
        }
    }
}
