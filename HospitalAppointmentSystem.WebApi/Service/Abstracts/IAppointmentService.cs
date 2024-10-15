using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;

namespace HospitalAppointmentSystem.WebApi.Service.Abstracts;

public interface IAppointmentService
{
    ReturnModel<List<AppointmentResponseDto>> GetAll();
    ReturnModel<AppointmentResponseDto> GetById(Guid id);
    ReturnModel<AppointmentResponseDto> Add(CreateAppointmentRequest request);
    ReturnModel<AppointmentResponseDto> Update(UpdateAppointmentRequest request);
    ReturnModel<AppointmentResponseDto> Delete(Guid id);
}