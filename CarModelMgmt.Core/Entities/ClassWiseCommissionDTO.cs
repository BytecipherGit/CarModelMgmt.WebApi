using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Core.Entities
{
    public class ClassWiseCommissionDTO
    {

        public string Class { get; set; }
        public string SalesManName { get; set; }
        public string Brand { get; set; }
        public int total_cars_sold { get; set; }
        public int Commission { get; set; }
        public int ModelPrice { get; set; }
        public int ClassCommision { get; set; }
        
    }
}
