using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class Staffs
    {
        public int staff_id { get; set; }
        public string staff_name { get; set; }
        public int phone_number { get; set; }
        public string gender { get; set; }
        public DateTime DOB { get; set; }
        public string blood_group { get; set; }

        public string qualification { get; set; }

        public bool isactive { get; set; }
        public int role_id { get; set; } // Foreign key

        public int specialization_id { get; set; } // Foreign key

        //Navigation Property as Object Oriented

        public Role Role { get; set; }

        public Specialization Specialization { get; set; }
    }
}
