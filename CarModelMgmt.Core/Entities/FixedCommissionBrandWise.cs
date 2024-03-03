using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Core.Entities
{
    public class FixedCommissionBrandWise
    {
        public string SalesManName { get; set; }
        public string Brand { get; set; }
        public int total_cars_sold { get; set; }
        public int FixedCommission { get; set; }
        public int TOTALCOMMISSION { get; set; }
       
    }
}
