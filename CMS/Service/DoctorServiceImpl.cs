using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Model;
using CMS.Repository;

namespace CMS.Service
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorServiceImpl(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<(int tokenNumber, decimal consultationFee)> BookAppointmentAsync(int patientId, int doctorId)
        {
            return await _doctorRepository.BookAppointmentAsync(patientId, doctorId);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _doctorRepository.GetAppointmentsByDoctorIdAsync(doctorId);
        }
        public async Task<int> GetDoctorIdByRoleAndUsernameAsync(int roleId, string username)
        {
            return await _doctorRepository.GetDoctorIdByRoleAndUsernameAsync(roleId, username);
        }
    }
}