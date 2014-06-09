﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iobserve_Azure.Models
{
    public class Observations
    {
        [Display(Name = "Date Name")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Risk Level")]
        public string  Risk_Level { get; set; }

        [Display(Name = "Risk Type")]
        public string  Risk_Type { get; set; }

        [Display(Name = "Supervisor Notified ?")]
        public string Supervisor_Notified { get; set; }

        [Display(Name = "Risk Eliminated ?")]
        public string  Risk_Eliminated { get; set; }
    
    }

}