using HospitalAppointmentSystem.WebApi.Models.Enums;

namespace HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;

public sealed record UpdateDoctorRequest(int Id, string Name, Branch Branch);