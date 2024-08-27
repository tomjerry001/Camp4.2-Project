using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CMS.Model;

namespace CMS.Repository
{
    public class DoctorRepositoryImpl : IDoctorRepository
    {
        private readonly string winconnString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            var doctors = new List<Doctor>();

            using (SqlConnection conn = new SqlConnection(winconnString))
            {
                await conn.OpenAsync();

                string query = "SELECT doctor_id, doctor_name, specalization, consultation_fee FROM Doctor";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            doctors.Add(new Doctor
                            {
                                doctor_id = reader.GetInt32(0),
                                doctor_name = reader.GetString(1),
                                specalization = reader.GetString(2),
                                consultation_fee = reader.GetInt32(3),
                            });
                        }
                    }
                }
            }

            return doctors;
        }

        public async Task<(int tokenNumber, int consultationFee)> BookAppointmentAsync(int patientId, int doctorId)
        {
            int tokenNumber;
            int consultationFee;

            using (SqlConnection conn = new SqlConnection(winconnString))
            {
                await conn.OpenAsync();

                string feeQuery = "SELECT consultation_fee FROM Doctor WHERE doctor_id = @DoctorId";
                using (SqlCommand feeCommand = new SqlCommand(feeQuery, conn))
                {
                    feeCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                    consultationFee = (int)await feeCommand.ExecuteScalarAsync();
                }

                Random rand = new Random();
                tokenNumber = rand.Next(1, 31);

                string insertQuery = @"
                    INSERT INTO Appointment (patient_id, doctor_id, token_number, created_at)
                    OUTPUT INSERTED.appointment_id
                    VALUES (@PatientId, @DoctorId, @TokenNumber, @CreatedAt)";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                {
                    insertCommand.Parameters.AddWithValue("@PatientId", patientId);
                    insertCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                    insertCommand.Parameters.AddWithValue("@TokenNumber", tokenNumber);
                    insertCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    await insertCommand.ExecuteScalarAsync();
                }
            }
            return (tokenNumber, consultationFee);
        }
        public async Task<int> GetDoctorIdByRoleAndUsernameAsync(int roleId, string username)
        {
            using (var connection = new SqlConnection(winconnString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("sp_GetDoctorIdByRoleAndUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.Parameters.AddWithValue("@UserName", username);

                    var doctorIdParam = new SqlParameter("@DoctorId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(doctorIdParam);

                    await command.ExecuteNonQueryAsync();

                    return (int)doctorIdParam.Value;
                }
            }
        }
        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            using (SqlConnection conn = new SqlConnection(winconnString))
            {
                string query = @"
        SELECT 
            P.name AS PatientName,
            P.patient_id AS PatientId,
            P.gender AS Gender,
            A.token_number AS TokenNumber,
            A.created_at AS CreatedAt
        FROM 
            [Cproject].[dbo].[Appointment] A
        INNER JOIN 
            [Cproject].[dbo].[Patient] P ON A.patient_id = P.patient_id
        WHERE 
            A.doctor_id = @DoctorId
        ORDER BY 
            A.created_at ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        List<AppointmentDto> appointments = new List<AppointmentDto>();

                        while (await reader.ReadAsync())
                        {
                            appointments.Add(new AppointmentDto
                            {
                                PatientId = Convert.ToInt32(reader["PatientId"]),
                                PatientName = reader["PatientName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                TokenNumber = Convert.ToInt32(reader["TokenNumber"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            });
                        }

                        return appointments;
                    }
                }
            }
        }

    }
}