using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iobserve_Azure.Models
{
  
    public class RiskLevel
    {
        public string Risk_Level { get; set; }
        public int Hazard_Count { get; set; }
        public int Behv_Count { get; set; }
        public int GoodJob_Count { get; set; }
        public string Risk_FullName { get; set; }
        public string Risk_ShortName { get; set; }
    }
}