using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.WebApi.Models;

public class Appointment : Entity<Guid>
{
    public string PatientName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}