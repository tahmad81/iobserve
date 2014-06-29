using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iobserve_Azure.Models
{
    public class Labels
    {
        public string Label_Id { get; set; }
        public string Label  { get; set; }
    }

    public static class LabelIds
    {

        public  const string REVIEWED = "C877C125-314A-4641-9C84-778C6FEB10E3";
        public  const string SUBMITTED = "61D9E040-81B1-4AFE-8955-6099A9349C97";
        public  const string PENDING = "C55160FB-417E-4370-87C9-EFCAB0374697";
        //public static const string CLOSED = "C877C125-314A-4641-9C84-778C6FEB10E3";


    }
}