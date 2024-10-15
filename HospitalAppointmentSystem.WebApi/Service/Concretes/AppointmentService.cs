using System.Net;
using AutoMapper;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;
using HospitalAppointmentSystem.WebApi.Repository.Abstracts;
using HospitalAppointmentSystem.WebApi.Service.Abstracts;
using Exception = System.Exception;

namespace HospitalAppointmentSystem.WebApi.Service.Concretes;

public class AppointmentService : IAppointmentService
{
    private IRepository<Appointment, Guid> _appointmentRepository;
    private IMapper _mapper;

    public AppointmentService(IRepository<Appointment, Guid> appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }
    
    
    public ReturnModel<List<AppointmentResponseDto>> GetAll()
    {
        var appointments = _appointmentRepository.GetAll();
        var appointmentsDto = _mapper.Map<List<AppointmentResponseDto>>(appointments);

        return new ReturnModel<List<AppointmentResponseDto>>
        {
            Success = true,
            Message = "Tüm randevular başarıyla alındı",
            Data = appointmentsDto,
            StatusCode = HttpStatusCode.OK
        };
    }

    public ReturnModel<AppointmentResponseDto> GetById(Guid id)
    {
        try
        {
            var appointment = _appointmentRepository.GetById(id);
            
            var appointmentDto = _mapper.Map<AppointmentResponseDto>(appointment);
            return new ReturnModel<AppointmentResponseDto>
            {
                Success = true,
                Data = appointmentDto,
                Message = "Randevu başarıyla bulundu.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return ReturnModelOfException(e);
        }
    }

    public ReturnModel<AppointmentResponseDto> Add(CreateAppointmentRequest request)
    {
        if (request.AppointmentDate <= DateTime.Now.AddDays(3))
        {
            return new ReturnModel<AppointmentResponseDto>
            {
                Success = false,
                Message = "Randevu tarihi, bugünden en az 3 gün sonrasına olmalıdır.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        
        var existingAppointments = _appointmentRepository.GetAll()
            .Count(x => x.DoctorId == request.DoctorId);

        if (existingAppointments >= 10)
        {
            return new ReturnModel<AppointmentResponseDto>
            {
                Success = false,
                Message = "Bu doktora en fazla 10 randevu alınabilir.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        try
        {
            CheckPatientName(request.PatientName);
            var appointment = _mapper.Map<Appointment>(request); 
            _appointmentRepository.Add(appointment);
        
            var appointmentDto = _mapper.Map<AppointmentResponseDto>(appointment); 
            return new ReturnModel<AppointmentResponseDto>
            {
                Success = true,
                Data = appointmentDto,
                Message = "Randevu başarıyla oluşturuldu.",
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return ReturnModelOfException(e);
        }
    }

    public ReturnModel<AppointmentResponseDto> Update(UpdateAppointmentRequest request)
    {
        try
        {
            var existingAppointment = _appointmentRepository.GetById(request.Id);
            if (existingAppointment == null)
            {
                throw new NotFoundException("Güncellenecek randevu bulunamadı.");
            }

            CheckPatientName(request.PatientName);
            var appointment = _mapper.Map<Appointment>(request);
            _appointmentRepository.Update(appointment);

            var appointmentDto = _mapper.Map<AppointmentResponseDto>(appointment);
            return new ReturnModel<AppointmentResponseDto>
            {
                Success = true,
                Data = appointmentDto,
                Message = "Randevu başarıyla güncellendi.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return ReturnModelOfException(e);
        }
    }

    public ReturnModel<AppointmentResponseDto> Delete(Guid id)
    {
        try
        {
            var appointment = _appointmentRepository.Delete(id);
            if (appointment == null)
            {
                throw new NotFoundException("Silinecek randevu bulunamadı.");
            }

            if (appointment.AppointmentDate < DateTime.Now)
            {
                _appointmentRepository.Delete(id);
                return new ReturnModel<AppointmentResponseDto>()
                {
                    Success = true,
                    Data = null,
                    Message = "Randevu tarihi geçmiş olduğu için silindi.",
                    StatusCode = HttpStatusCode.OK
                };
                
            }
            
            var appointmentDto = _mapper.Map<AppointmentResponseDto>(appointment);
            return new ReturnModel<AppointmentResponseDto>
            {
                Success = true,
                Data = appointmentDto,
                Message = "Randevu başarıyla silindi.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return ReturnModelOfException(e);
        }
    }
    
    
    private void CheckPatientName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 1)
        {
            throw new ValidationException("Hasta ismi minimum 1 karakterli olmalıdır.");
        }
    }
    private ReturnModel<AppointmentResponseDto> ReturnModelOfException(Exception e)
    {
        if (e.GetType() == typeof(NotFoundException))
        {
            return new ReturnModel<AppointmentResponseDto>
            {
                Data = null,
                Message = e.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound
            };
        }
        
        if(e.GetType()== typeof(ValidationException))
        {
            return new ReturnModel<AppointmentResponseDto>
            {
                Data = null,
                Message = e.Message,
                Success = false,
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        return new ReturnModel<AppointmentResponseDto>
        {
            Data = null,
            Message = e.Message,
            Success = false,
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}