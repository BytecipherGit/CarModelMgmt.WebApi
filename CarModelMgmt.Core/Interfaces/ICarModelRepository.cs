using CarModelMgmt.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Core.Interfaces
{
    public interface ICarModelRepository
    {
        Task<int> SaveCarDetal(CarModelDTO dto);
        Task UpdateCarDetal(CarModelDTO dto);
        Task UpdateCarStatus(CarModelDTO dto);
        Task<IEnumerable<CarModelDTO>> GetCarDetal(/*string modelname, string modelCode*/);
        //Task<IEnumerable<CoverageAreaDTO>> GetEventCoverageAreas(int userId, int eventId);
        //Task<IEnumerable<EventParticipateCriteriaRulesType>> GetEventParticipationRules(int userId, int eventId);
        //Task<GetEventDetailsDTO> GetEventDetails(int userId, int eventId);
    }
}
