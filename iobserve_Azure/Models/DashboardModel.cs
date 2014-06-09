using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iobserve_Azure.Models
{
    public class DashboardModel
    {
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
    }
}