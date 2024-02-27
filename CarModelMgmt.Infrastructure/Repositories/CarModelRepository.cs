using CarModelMgmt.Core.Entities;
using CarModelMgmt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Infrastructure.Repositories
{
    public class CarModelRepository : ICarModelRepository
    {
        public Task<IEnumerable<CarModelDTO>> GetCarDetal(string modelname, string modelCode)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveCarDetal(CarModelDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCarDetal(CarModelDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCarStatus(CarModelDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
