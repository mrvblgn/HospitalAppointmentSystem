using System.Net;
using AutoMapper;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Responses;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Models.Enums;
using HospitalAppointmentSystem.WebApi.Models.ReturnModels;
using HospitalAppointmentSystem.WebApi.Repository.Abstracts;
using HospitalAppointmentSystem.WebApi.Service.Abstracts;

namespace HospitalAppointmentSystem.WebApi.Service.Concretes;

public class DoctorService : IDoctorService
{
    private IRepository<Doctor, int> _doctorRepository;
    private IMapper _mapper;

    public DoctorService(IRepository<Doctor, int> doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }
    
    public ReturnModel<List<DoctorResponseDto>> GetAll()
    {
        var doctors = _doctorRepository.GetAll();
        var doctorDtos = _mapper.Map<List<DoctorResponseDto>>(doctors);
        return new ReturnModel<List<DoctorResponseDto>>
        {
            Success = true,
            Data = doctorDtos,
            Message = "Tüm doktorlar başarıyla alındı.",
            StatusCode = HttpStatusCode.OK
        };
    }

    public ReturnModel<DoctorResponseDto> GetById(int id)
    {
        try
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                throw new NotFoundException("Doktor bulunamadı.");
            }

            var doctorDto = _mapper.Map<DoctorResponseDto>(doctor);
            return new ReturnModel<DoctorResponseDto>
            {
                Success = true,
                Data = doctorDto,
                Message = "Doktor başarıyla bulundu.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return ReturnModelOfException(ex);
        }
    }

    public ReturnModel<DoctorResponseDto> Add(CreateDoctorRequest request)
    {
        try
        {
            CheckDoctorName(request.Name); 
            var doctor = _mapper.Map<Doctor>(request);
            _doctorRepository.Add(doctor);

            var doctorDto = _mapper.Map<DoctorResponseDto>(doctor);
            return new ReturnModel<DoctorResponseDto>
            {
                Success = true,
                Data = doctorDto,
                Message = "Doktor başarıyla eklendi.",
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (Exception ex)
        {
            return ReturnModelOfException(ex);
        }
    }

    public ReturnModel<DoctorResponseDto> Update(UpdateDoctorRequest request)
    {
        try
        {
            var existingDoctor = _doctorRepository.GetById(request.Id);
            if (existingDoctor == null)
            {
                throw new NotFoundException("Güncellenecek doktor bulunamadı.");
            }

            CheckDoctorName(request.Name); 
            var doctor = _mapper.Map<Doctor>(request);
            _doctorRepository.Update(doctor);

            var doctorDto = _mapper.Map<DoctorResponseDto>(doctor);
            return new ReturnModel<DoctorResponseDto>
            {
                Success = true,
                Data = doctorDto,
                Message = "Doktor başarıyla güncellendi.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return ReturnModelOfException(ex);
        }
    }

    public ReturnModel<DoctorResponseDto> Delete(int id)
    {
        try
        {
            var doctor = _doctorRepository.Delete(id);
            if (doctor == null)
            {
                throw new NotFoundException("Silinecek doktor bulunamadı.");
            }

            var doctorDto = _mapper.Map<DoctorResponseDto>(doctor);
            return new ReturnModel<DoctorResponseDto>
            {
                Success = true,
                Data = doctorDto,
                Message = "Doktor başarıyla silindi.",
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return ReturnModelOfException(ex);
        }
    }
    
    
    private void CheckDoctorName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 1)
        {
            throw new ValidationException("Doktor ismi minimum 1 karakterli olmalıdır.");
        }
    }
    private ReturnModel<DoctorResponseDto> ReturnModelOfException(Exception e)
    {
        if (e is NotFoundException)
        {
            return new ReturnModel<DoctorResponseDto>
            {
                Data = null,
                Message = e.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound
            };
        }

        return new ReturnModel<DoctorResponseDto>
        {
            Data = null,
            Message = e.Message,
            Success = false,
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}