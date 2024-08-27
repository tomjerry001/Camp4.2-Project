using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Model;

namespace CMS.Service
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<(int tokenNumber, decimal consultationFee)> BookAppointmentAsync(int patientId, int doctorId);
        Task<int> GetDoctorIdByRoleAndUsernameAsync(int roleId, string username);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId);
    }
}