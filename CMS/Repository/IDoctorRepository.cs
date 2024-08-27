using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Model;

namespace CMS.Repository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<(int tokenNumber, int consultationFee)> BookAppointmentAsync(int patientId, int doctorId);
        Task<int> GetDoctorIdByRoleAndUsernameAsync(int roleId, string username);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId);

    }
}