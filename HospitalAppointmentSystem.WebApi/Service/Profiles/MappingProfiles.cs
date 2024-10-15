using AutoMapper;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Models;

namespace HospitalAppointmentSystem.WebApi.Service.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDoctorRequest, Doctor>();
        CreateMap<UpdateDoctorRequest, Doctor>();
        
        CreateMap<CreateAppointmentRequest, Appointment>();
        CreateMap<UpdateAppointmentRequest, Appointment>();
        
        CreateMap<Doctor, DoctorResponseDto>();
        CreateMap<Appointment, AppointmentResponseDto>();
    }
}