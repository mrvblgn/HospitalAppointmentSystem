namespace HospitalAppointmentSystem.WebApi.Exceptions;

public class ValidationException(string msg) : Exception(msg);