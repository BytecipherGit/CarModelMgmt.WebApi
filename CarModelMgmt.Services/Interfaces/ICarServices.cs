using CarModelMgmt.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Services.Interfaces
{
    public interface ICarServices
    {
        Task<IEnumerable<CarModelDTO>> GetCarDetal(/*string modelname, string modelCode*/);
        Task<int> SaveCarDetal(CarModelDTO dto);
    }
}
