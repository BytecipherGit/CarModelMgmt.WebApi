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
        private readonly DapperHelper _dapperHelper;
        public CarModelRepository(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<IEnumerable<CarModelDTO>> GetCarDetal(/*string modelname, string modelCode*/)
        {
            try
            {
               
                return await _dapperHelper.QueryAsync<CarModelDTO>("GetAllCarDetails");

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
                var parameters = new
                {
                    Brand = dto.Brand,
                    Class = dto.Class,
                    ModelName = dto.ModelName,
                    ModelCode = dto.ModelCode,
                    Description = dto.Description,
                    Features = dto.Features,
                    Price = dto.Price,
                    DateOfManufacturing = dto.DateOfManufacturing,
                    Active = dto.Active,
                    SortOrder = dto.SortOrder,
                    ModelImage = dto.ModelImageUrl,
                   

                };

                object result = await _dapperHelper.ExecuteScalarAsync("InsertCarModel", parameters);
                int id = result != null ? Convert.ToInt32(result) : 0;
                return id;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
