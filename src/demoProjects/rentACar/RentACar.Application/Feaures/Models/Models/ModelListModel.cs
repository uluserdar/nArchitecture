using Core.Persistence.Paging;
using RentACar.Application.Feaures.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Feaures.Models.Models
{
    public class ModelListModel:BasePageableModel
    {
        public IList<ModelListDto> Items { get; set; }
    }
}
