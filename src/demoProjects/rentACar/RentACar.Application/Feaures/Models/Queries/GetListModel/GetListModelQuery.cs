﻿using AutoMapper;
using Core.Application.Requests;
using MediatR;
using RentACar.Application.Feaures.Models.Models;
using RentACar.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Feaures.Models.Queries.GetListModel
{
    public class GetListModelQuery:IRequest<ModelListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListModelQueryHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
            {

            }
        }
    }
}
