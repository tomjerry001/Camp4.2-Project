using CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public interface IPatientService
    {
        Task AddPatientAsync(Patient patient);

        Task<Patient> SearchPatientAsync(string name, string phone_number);
    }
}
