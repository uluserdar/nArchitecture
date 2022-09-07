using Core.Application.Requests;
using MediatR;
using RentACar.Application.Feaures.Models.Models;
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
            public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
            {

            }
        }
    }
}
