using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentACar.Application.Feaures.Models.Models;
using RentACar.Application.Feaures.Models.Queries.GetListModel;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Feaures.Models.Queries.GetListModelByDynamic
{
    public class GetListModelByDynamicQuery : IRequest<ModelListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListModelByDynamicQueryHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListModelByDynamicQueryHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(
                  dynamic:request.Dynamic, 
                  include:m => m.Include(c => c.Brand),
                  index: request.PageRequest.Page,
                  size: request.PageRequest.PageSize);

                ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
                return mappedModels;
            }
        }
    }
}
