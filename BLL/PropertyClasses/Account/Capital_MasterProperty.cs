using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.PropertyClasses.Master
{
    public class Capital_MasterProperty
    {
        public Int64 Capital_Code { get; set; }
        public string Capital_Name { get; set; }
        public string Remark { get; set; }
        public Int64 Capital_Entry_Code { get; set; }
        public Int64 Party_Code { get; set; }
        public decimal Amount { get; set; }
        public string Entry_Date { get; set; }
        public string Insert_Date { get; set; }
    }
}
