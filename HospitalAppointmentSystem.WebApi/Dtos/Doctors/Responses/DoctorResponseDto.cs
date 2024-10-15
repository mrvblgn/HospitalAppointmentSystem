using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;

public sealed class DoctorResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Branch { get; set; }
    
    public DoctorResponseDto() { }
};