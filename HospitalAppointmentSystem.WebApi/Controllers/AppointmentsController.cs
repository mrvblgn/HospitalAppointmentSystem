using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Requests;
using HospitalAppointmentSystem.WebApi.Dtos.Appointments.Responses;
using HospitalAppointmentSystem.WebApi.Models;
using HospitalAppointmentSystem.WebApi.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _appointmentService.GetAll();
        if (!result.Success)
        {
            return StatusCode((int)result.StatusCode, result);
        }
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetById([FromQuery]Guid id)
    {
        var result = _appointmentService.GetById(id);
        if (!result.Success)
        {
            return StatusCode((int)result.StatusCode, result);
        }
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody]CreateAppointmentRequest request)
    {
        var added = _appointmentService.Add(request);
        return Ok(added);
    }

    [HttpPut("update")]
    public IActionResult Update(Guid id, [FromBody] UpdateAppointmentRequest request)
    {
        var updated = _appointmentService.Update(request);
        return Ok(updated);
    }

    [HttpDelete("delete")]
    public IActionResult Delete(Guid id)
    {
        var result = _appointmentService.Delete(id);
        return Ok(result);
    }
}