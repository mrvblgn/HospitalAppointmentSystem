namespace HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;

public class AppointmentResponseDto{
    public Guid Id { get; init; }
    public string PatientName { get; init; }
    public DateTime AppointmentDate { get; init; }
    public string DoctorName { get; init; }
    
    public AppointmentResponseDto() { }
    
};