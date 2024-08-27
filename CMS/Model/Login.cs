using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Model
{
    public class Login
    {
        public string UserName { get; set; }

        public string password { get; set; }

        public int staff_id { get; set; }

        //Navigation Property as Object Oriented
        public Staffs Staffs { get; set; }
    }
}
