using CMS.Model;
using CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public class PatientServiceImpl : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientServiceImpl(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _patientRepository.AddPatientAsync(patient);
        }

        public async Task<Patient> SearchPatientAsync(string name, string phone_number)
        {
            return await _patientRepository.SearchPatientAsync(name, phone_number);
        }
    }

}