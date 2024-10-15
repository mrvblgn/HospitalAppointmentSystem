using HospitalAppointmentSystem.WebApi.Contexts;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Repository.Abstracts;
using HospitalAppointmentSystem.WebApi.Repository.Concretes;
using HospitalAppointmentSystem.WebApi.Service.Abstracts;
using HospitalAppointmentSystem.WebApi.Service.Concretes;
using HospitalAppointmentSystem.WebApi.Service.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MsSqlContext>(); // Dependency Injection mekanizması aracılığıyla DbContext nesnelerinin oluşturulmasını ve yönetilmesini sağlar
builder.Services.AddScoped<IRepository<Doctor, int>, EfDoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IRepository<Appointment, Guid>, EfAppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();