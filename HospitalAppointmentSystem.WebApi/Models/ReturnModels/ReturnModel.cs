using System.Net;

namespace HospitalAppointmentSystem.WebApi.Models.ReturnModels;

public class ReturnModel<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    

    public override string ToString()
    {
        return $"Success: {Success}, Message: {Message}, Data: {Data}, StatusCode: {StatusCode}";
    }
}