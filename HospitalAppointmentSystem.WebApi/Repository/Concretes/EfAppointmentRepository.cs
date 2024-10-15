using HospitalAppointmentSystem.WebApi.Contexts;
using HospitalAppointmentSystem.WebApi.Exceptions;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Repository.Concretes;

public class EfAppointmentRepository : IRepository<Appointment, Guid>
{
    private MsSqlContext _context;

    public EfAppointmentRepository(MsSqlContext context)
    {
        _context = context;
    }
    
    public List<Appointment> GetAll()
    {
        return _context.Appointments.Include(a => a.Doctor).ToList();
    }

    public Appointment? GetById(Guid id)
    {
        Appointment? appointment = _context.Appointments
            .AsNoTracking()
            .Include(a => a.Doctor)
            .SingleOrDefault(a => a.Id == id);

        if (appointment == null)
        {
            throw new NotFoundException("Aradığınız randevu bulunamadı.");
        }
        
        return appointment;
    }

    public Appointment? Add(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
        
        return appointment;
    }

    public Appointment? Update(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        _context.SaveChanges();
        
        return appointment;
    }

    public Appointment? Delete(Guid id)
    {
        Appointment? appointment = GetById(id);
        
        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
        
        return appointment;
    }
}