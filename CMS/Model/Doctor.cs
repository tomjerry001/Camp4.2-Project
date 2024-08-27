namespace CMS.Model
{
    public class Doctor
    {
        public int doctor_id { get; set; }
        public string doctor_name { get; set; }
        public string specalization { get; set; } // Ensure this matches the database column
        public int consultation_fee { get; set; } // Ensure this matches the database column
    }
}
