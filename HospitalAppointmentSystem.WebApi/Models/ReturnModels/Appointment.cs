namespace HospitalAppointmentSystem.WebApi.Models;

public class Appointment : Entity<Guid>
{
    public string PatientName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DoctorId { get; set; }
}