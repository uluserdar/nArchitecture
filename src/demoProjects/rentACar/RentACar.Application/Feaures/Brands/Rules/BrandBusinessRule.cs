using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Feaures.Brands.Rules
{
    public class BrandBusinessRule
    {
        private readonly IBrandRepository _brandRepsitory;

        public BrandBusinessRule(IBrandRepository brandRepsitory)
        {
            _brandRepsitory = brandRepsitory;
        }

        public async Task BrandNameCanNotBeDublicatedWhenInserted(string name)
        {
            IPaginate<Brand> result = await _brandRepsitory.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Brand name exists");
        }

        public void BrandShouldExistsWhenRequested(Brand brand)
        {
            if (brand==null) throw new BusinessException("Requestes brand does not exists");
        }
    }
}
