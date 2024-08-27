using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class Patient
    {

        //properties
        public int patient_id { get; set; }
        public string name { get; set; }
        public DateTime DOB { get; set; }
        public string gender { get; set; }

        public string blood_group { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
    }
}
