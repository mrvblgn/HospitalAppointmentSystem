using System.ComponentModel.DataAnnotations;
using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Models;

public class Doctor : Entity<int>
{
    public string Name { get; set; }
    public Branch Branch { get; set; }
    public List<Appointment> Patients { get; set; }
}