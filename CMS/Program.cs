using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CMS.Model;
using CMS.Repository;
using CMS.Service;
using CMS.Utility;

namespace CMS_Project
{

    public class Program
    {
        static string _userName;
        static int _roleId;
        static int _doctorId;
        private static IPatientService _patientService;
        private static IDoctorService _doctorService;

        static async Task Main(string[] args)
        {
            _patientService = new PatientServiceImpl(new PatientRepositoryImpl());
            _doctorService = new DoctorServiceImpl(new DoctorRepositoryImpl());

            Console.WriteLine("                          ----------------------------------------------------------------  ");
            Console.WriteLine("                         |       JUBILEE    MISSION    MEDICAL   COLLEGE                  | ");
            Console.WriteLine("                          ----------------------------------------------------------------  ");

            Console.WriteLine("                                            ");
            Console.WriteLine("                                            ");
            Console.WriteLine("                                            ");

            // Main menu
            while (true)
            {
                Console.WriteLine("                                         -----------------------------------------------------");
                Console.WriteLine("                                         *    L  O  G  I  N   D  A  S  H  B  O  A  R  D *     ");
                Console.WriteLine("                                         -----------------------------------------------------");

                // UserName
                Console.Write("Enter UserName  :  ");
                string username = Console.ReadLine();



                // Check validations
                if (!CustomValidation.IsValidUserName(username))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid User Name, Try Again");

                    continue;

                }

                // Password
                Console.Write("Enter Password  :  ");
                string password = CustomValidation.ReadPassword();

                // Check validations
                if (!CustomValidation.IsValidPassword(password))
                {
                    Console.WriteLine("Invalid Password, Try Again");
                    continue;
                }

                // After UI validation
                try
                {
                    ILoginService loginService = new LoginServiceImpl(new LoginRepositoryImpl());
                    int roleId = await loginService.AuthenticateUserAsync(username, password);

                    // Dashboard
                    if (roleId >= 1)
                    {
                        switch (roleId)
                        {
                            case 1:
                                await ShowReceptionistMenuAsync();
                                break;
                            case 2:
                                _roleId = roleId;
                                _userName = username;
                                int currentDoctorId = await _doctorService.GetDoctorIdByRoleAndUsernameAsync(_roleId, _userName);
                                await ShowDoctorMenuAsync();

                                _roleId = roleId;
                                _userName = username;

                                break;
                            default:
                                Console.WriteLine("Invalid role: ACCESS DENIED!");
                                break;
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid credentials");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }


            }
        }

        public static async Task ShowReceptionistMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--------------------------");
                Console.WriteLine("* Receptionist Dashboard *");
                Console.WriteLine("--------------------------");

                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Search Patient");          
                Console.WriteLine("3. Logout");

                Console.Write("\nSelect an option : ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await AddPatientAsync();
                        break;
                    case "2":
                        await SearchPatientAsync();
                        break;
                   // case "3":
                       // await BookAppointmentAsync();
                       // break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                Console.ReadKey();
            }
        }

        private static async Task AddPatientAsync()
        {
            var patient = new Patient();

            // Patient Name
            while (true)
            {
                Console.Write("Patient Name  : ");
                patient.name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(patient.name) || !patient.name.All(char.IsLetter))
                {
                    Console.WriteLine("Please enter a valid name containing only alphabetic characters.");
                }
                else
                {
                    break;
                }
            }

            // Date of Birth (DOB)
            while (true)
            {
                Console.Write("DOB (yyyy-mm-dd) : ");
                string dobInput = Console.ReadLine();
                if (DateTime.TryParseExact(dobInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dob))
                {
                    patient.DOB = dob;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                }
            }

            // Gender
            while (true)
            {
                Console.WriteLine("Gender:");
                Console.WriteLine("1. Male");
                Console.WriteLine("2. Female");
                Console.Write("Choose an option (1/2) : ");
                string genderInput = Console.ReadLine();
                switch (genderInput)
                {
                    case "1":
                        patient.gender = "Male";
                        break;
                    case "2":
                        patient.gender = "Female";
                        break;
                    default:
                        Console.WriteLine("Please select a valid option for gender.");
                        continue;
                }
                break;
            }

           
            while (true)
            {
                Console.WriteLine("Blood group:");
                Console.WriteLine("1. A+");
                Console.WriteLine("2. A-");
                Console.WriteLine("3. B+");
                Console.WriteLine("4. B-");
                Console.WriteLine("5. O+");
                Console.Write("Choose an option (1-5) : ");
                string bloodGroupInput = Console.ReadLine();
                switch (bloodGroupInput)
                {
                    case "1":
                        patient.blood_group = "A+";
                        break;
                    case "2":
                        patient.blood_group = "A-";
                        break;
                    case "3":
                        patient.blood_group = "B+";
                        break;
                    case "4":
                        patient.blood_group = "B-";
                        break;
                    case "5":
                        patient.blood_group = "O+";
                        break;
                    default:
                        Console.WriteLine("Please select a valid option for blood group.");
                        continue;
                }
                break;
            }

           
            while (true)
            {
                Console.Write("Phone Number  : ");
                patient.phone_number = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(patient.phone_number))
                {
                    Console.WriteLine("Please enter the patient's phone number.");
                }
                else if (!patient.phone_number.All(char.IsDigit))
                {
                    Console.WriteLine("Phone number should contain only digits.");
                }
                else if (patient.phone_number.Length != 10)
                {
                    Console.WriteLine("Phone number should be exactly 10 digits.");
                }
                else if (!new[] { '6', '7', '8', '9' }.Contains(patient.phone_number[0]))
                {
                    Console.WriteLine("Indian phone numbers should start with 6, 7, 8, or 9.");
                }
                else
                {
                    break;
                }
            }

            // Address
            while (true)
            {
                Console.Write("Address       : ");
                patient.address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(patient.address))
                {
                    Console.WriteLine("Please enter the patient's address.");
                }
                else
                {
                    break;
                }
            }

            try
            {
                await _patientService.AddPatientAsync(patient);
                Console.WriteLine("Patient Added Successfully");

                var patients = await _patientService.SearchPatientAsync(patient.name, patient.phone_number);
                if (patients != null)
                {
                    Console.WriteLine($"Patient ID   : {patients.patient_id}");
                }

                Console.WriteLine("1. Save and Book Appointment");
                Console.WriteLine("2. Save and Exit");

                Console.WriteLine("\n Select an option :");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await BookAppointmentAsync();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

            }
            catch (FormatException fe)
            {
                Console.WriteLine("Invalid input format: " + fe.Message);
            }
            catch (SqlException se)
            {
                Console.WriteLine("Database error occurred: " + se.Message);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("A required field was not provided: " + ane.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


        private static async Task SearchPatientAsync()
        {
            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phone_number = Console.ReadLine();

            try
            {
                var patient = await _patientService.SearchPatientAsync(name, phone_number);

                if (patient != null)
                {
                    Console.WriteLine("Patient Found");

                  
                    Console.WriteLine("+-------------+---------------------------------+");
                    Console.WriteLine("| Column Name       | Values                    |");
                    Console.WriteLine("+-------------+---------------------------------+");
                    Console.WriteLine($"| Patient ID  | {patient.patient_id,-31} |");
                    Console.WriteLine($"| Name        | {patient.name,-31} |");
                    Console.WriteLine($"| DOB         | {patient.DOB:yyyy-MM-dd,-31} |");
                    Console.WriteLine($"| Gender      | {patient.gender,-31} |");
                    Console.WriteLine($"| Blood Group | {patient.blood_group,-31} |");
                    Console.WriteLine($"| Phone Number| {patient.phone_number,-31} |");
                    Console.WriteLine($"| Address     | {patient.address,-31} |");
                    Console.WriteLine("+-------------+---------------------------------+");
                }
                else
                {
                    Console.WriteLine("Patient Not Found");
                }

                Console.WriteLine("1.Book Appointment");
                Console.WriteLine("2.Exit");
                Console.WriteLine("\nSelect an option :");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        BookAppointmentAsync();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching for the patient: {ex.Message}");
            }
        }

        private static async Task BookAppointmentAsync()
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();

                Console.WriteLine("Available Doctors");
                Console.WriteLine("------------------");

                // Display table header
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("| Doctor ID |    Doctor Name    | Specialization | Consultation Fee |");
                Console.WriteLine("---------------------------------------------------------------------");

                // Display each doctor's information in a table format
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"| {doctor.doctor_id,-9} | {doctor.doctor_name,-15} | {doctor.specalization,-14} | {doctor.consultation_fee,-17}  |");
                }

                Console.WriteLine("---------------------------------------------------------------------");

                Console.Write("Enter Patient ID: ");
                int patientId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Doctor ID: ");
                int doctorId = Convert.ToInt32(Console.ReadLine());
                var (tokenNumber, consultationFee) = await _doctorService.BookAppointmentAsync(patientId, doctorId);
                Console.WriteLine("Appointment booked successfully.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("       Bill Details             ");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("                                ");
                Console.WriteLine($"Your token number is: {tokenNumber}");
                Console.WriteLine($"Consultation Fee: {consultationFee}");
                Console.WriteLine("--------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while booking an appointment: {ex.Message}");
            }
        }

        public static async Task ShowDoctorMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------------------");
                Console.WriteLine("* Doctor's Dashboard *");
                Console.WriteLine("----------------------");

                Console.WriteLine("1. View My Appointments");
                Console.WriteLine("2. Logout");

                Console.Write("\nSelect an option : ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":                   
                        int doctorId = await _doctorService.GetDoctorIdByRoleAndUsernameAsync(_roleId, _userName); 
                        await ViewPatientAppointmentsAsync(doctorId);
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
                Console.ReadKey();
            }
        }

       
       


        private static async Task ViewPatientAppointmentsAsync(int doctorId)
        {
            try
            {
                
                var appointments = await _doctorService.GetAppointmentsByDoctorIdAsync(doctorId);

                Console.WriteLine("Today Appointments");
                Console.WriteLine("------------------");

                if (appointments.Any())
                {
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine("Patient Id   |   Patient Name  | Gender | Token Number | Appointment Date      |");
                    Console.WriteLine("--------------------------------------------------------------------------------");

                    foreach (var appointment in appointments)
                    {
                        Console.WriteLine($"|{appointment.PatientId,-9} | {appointment.PatientName,-14} | {appointment.Gender,-6} | {appointment.TokenNumber,-12} | {appointment.CreatedAt,-24} |");
                    }

                    Console.WriteLine("--------------------------------------------------------------------------------");

                        Console.Write("Enter the Patient ID to add a prescription: ");
                        int patientId = int.Parse(Console.ReadLine());

                        await AddPrescriptionAsync(patientId);
                    
                }
                else
                {
                    Console.WriteLine("No appointments found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving appointments: {ex.Message}");
            }
        }

        private static async Task AddPrescriptionAsync(int patientId)
        {
            try
            {
                var medicines = new List<string> { "Acetaminophen", "Ibuprofen", "Amoxicillin", "Saline Nasal Drops", "Cetirizine" };
                var selectedMedicines = new List<string>();

                Console.WriteLine("Available Medicines:");
                Console.WriteLine("+-----+-------------------+");
                Console.WriteLine("| No. | Medicine           |");
                Console.WriteLine("+-----+-------------------+");
                for (int i = 0; i < medicines.Count; i++)
                {
                    Console.WriteLine($"| {i + 1,2}  | {medicines[i],-18} |");
                }
                Console.WriteLine("+-----+-------------------+");

                Console.Write("Select the medicines you want to prescribe: ");
                string[] selectedIndexes = Console.ReadLine().Split(',');

                foreach (string index in selectedIndexes)
                {
                    if (int.TryParse(index.Trim(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= medicines.Count)
                    {
                        selectedMedicines.Add(medicines[selectedIndex - 1]);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid selection: {index}");
                    }
                }

                Console.Write("Do you want to add a test? (yes/no): ");
                string addTest = Console.ReadLine().ToLower();

                var selectedTests = new List<string>();
                if (addTest == "yes")
                {
                    var tests = new List<string> { "Blood Test", "X-Ray", "Urine Test", "MRI", "CT Scan", "EKG", "Thyroid Function Test" };

                    Console.WriteLine("Available Tests:");
                    Console.WriteLine("+-----+---------------------+");
                    Console.WriteLine("| No. | Test                |");
                    Console.WriteLine("+-----+---------------------+");
                    for (int i = 0; i < tests.Count; i++)
                    {
                        Console.WriteLine($"| {i + 1,2}  | {tests[i],-19} |");
                    }
                    Console.WriteLine("+-----+---------------------+");

                    Console.Write("Select the test you want to prescribe: ");
                    string[] selectedTestIndexes = Console.ReadLine().Split(',');

                    foreach (string index in selectedTestIndexes)
                    {
                        if (int.TryParse(index.Trim(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= tests.Count)
                        {
                            selectedTests.Add(tests[selectedIndex - 1]);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid selection: {index}");
                        }
                    }

                    if (selectedTests.Count > 0)
                    {
                        
                    }
                }

                Console.Write("Enter notes for the patient: ");
                string notes = Console.ReadLine();

                

                Console.WriteLine("Prescription added successfully.");

               
                if (selectedMedicines.Count > 0)
                {
                    Console.WriteLine("\nPrescribed Medicines:");
                    Console.WriteLine("+-----+-------------------+");
                    Console.WriteLine("| No. | Medicine           |");
                    Console.WriteLine("+-----+-------------------+");
                    for (int i = 0; i < selectedMedicines.Count; i++)
                    {
                        Console.WriteLine($"| {i + 1,2}  | {selectedMedicines[i],-18} |");
                    }
                    Console.WriteLine("+-----+-------------------+");
                }

             
                if (selectedTests.Count > 0)
                {
                    Console.WriteLine("\nPrescribed Tests:");
                    Console.WriteLine("+-----+---------------------+");
                    Console.WriteLine("| No. | Test                |");
                    Console.WriteLine("+-----+---------------------+");
                    for (int i = 0; i < selectedTests.Count; i++)
                    {
                        Console.WriteLine($"| {i + 1,2}  | {selectedTests[i],-19} |");
                    }
                    Console.WriteLine("+-----+---------------------+");
                }

                Console.WriteLine("\nThank you for consulting with us. We wish you good health and a speedy recovery!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the prescription: {ex.Message}");
            }
        }




    }
}