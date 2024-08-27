using CMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class PatientRepositoryImpl : IPatientRepository
    {
        string winconnString = ConfigurationManager.ConnectionStrings["Cswin"].ConnectionString;

        public async Task AddPatientAsync(Patient patient)
        {
            using (SqlConnection conn = new SqlConnection(winconnString))
            {
                await conn.OpenAsync();

                string query = @"INSERT INTO Patient (name, DOB, gender, blood_group, phone_number, address) 
                             VALUES (@p_name, @p_DOB, @p_gender, @p_bloodgroup, @p_phonenumber, @p_address)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@p_name", patient.name);
                    command.Parameters.AddWithValue("@p_DOB", patient.DOB);
                    command.Parameters.AddWithValue("@p_gender", patient.gender);
                    command.Parameters.AddWithValue("@p_bloodgroup", patient.blood_group);
                    command.Parameters.AddWithValue("@p_phonenumber", patient.phone_number);
                    command.Parameters.AddWithValue("@p_address", patient.address);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Patient> SearchPatientAsync(string name, string phone_number)
        {
            using (SqlConnection conn = new SqlConnection(winconnString))
            {
                string query = "SELECT * FROM Patient WHERE name = @patientName and phone_number = @phoneNumber";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@patientName", name);
                    command.Parameters.AddWithValue("@phoneNumber", phone_number);

                    await conn.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Patient
                            {
                                patient_id = reader.GetInt32(0),
                                name = reader["name"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                gender = reader["gender"].ToString(),
                                blood_group = reader["blood_group"].ToString(),
                                phone_number = reader["phone_number"].ToString(),
                                address = reader["address"].ToString()
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }


            }

        }
    }
}