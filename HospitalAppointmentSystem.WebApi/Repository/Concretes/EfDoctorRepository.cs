using HospitalAppointmentSystem.WebApi.Contexts;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Repository.Concretes;

public class EfDoctorRepository : IRepository<Doctor, int>
{
    private MsSqlContext _context;

    public EfDoctorRepository(MsSqlContext context)
    {
        _context = context;
    }
    
    public List<Doctor> GetAll()
    {
        return _context.Doctors.Include(x => x.Patients).ToList();
    }

    public Doctor? GetById(int id)
    {
        Doctor? doctor = _context.Doctors
            .AsNoTracking() // veriler izlenmez
            .Include(x => x.Patients)
            .SingleOrDefault(x => x.Id == id);
        
        if (doctor == null)
        {
            throw new NotFoundException("Aradığınız doktor bulunamadı.");
        }
        
        return doctor;
    }

    public Doctor? Add(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
        
        return doctor;
    }

    public Doctor? Update(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        _context.SaveChanges();
        
        return doctor;
    }

    public Doctor? Delete(int id)
    {
        Doctor? doctor = GetById(id);
        
        _context.Doctors.Remove(doctor);
        _context.SaveChanges();
        
        return doctor;
    }
}