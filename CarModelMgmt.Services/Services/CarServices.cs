using CarModelMgmt.Core.Entities;
using CarModelMgmt.Core.Interfaces;
using CarModelMgmt.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Services.Services
{
    public class CarServices : ICarServices
    {
        private readonly ICarModelRepository _carRepository;
        public CarServices(ICarModelRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<IEnumerable<CarModelDTO>> GetCarDetal()
        {
            try
            {
                return await _carRepository.GetCarDetal();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<FixedCommissionBrandWise>> GetCommissionDetal()
        {
            try
            {
                return await _carRepository.GetCommissionDetal();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ClassWiseCommissionDTO>> GetCommissionDetalClassWise(string Class)
        {
            try
            {
                return await _carRepository.GetCommissionDetalClassWise(Class);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SaveCarDetal(CarModelDTO dto)
        {
            try
            {
                return await _carRepository.SaveCarDetal(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
