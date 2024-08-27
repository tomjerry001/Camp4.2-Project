using System;

public class AppointmentDto
{
    public int PatientId {  get; set; }
    public string PatientName { get; set; }
    public string Gender { get; set; }
    public int TokenNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}
