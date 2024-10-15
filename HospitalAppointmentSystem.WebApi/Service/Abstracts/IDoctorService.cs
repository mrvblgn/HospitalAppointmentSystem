using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;

namespace HospitalAppointmentSystem.WebApi.Service.Abstracts;

public interface IDoctorService
{
    ReturnModel<List<DoctorResponseDto>> GetAll();
    ReturnModel<DoctorResponseDto> GetById(int id);
    ReturnModel<DoctorResponseDto> Add(CreateDoctorRequest request);
    ReturnModel<DoctorResponseDto> Update(UpdateDoctorRequest request);
    ReturnModel<DoctorResponseDto> Delete(int id);
}