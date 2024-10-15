using HospitalAppointmentSystem.WebApi.Dtos.Doctors.Requests;
using HospitalAppointmentSystem.WebApi.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _doctorService.GetAll();
        return Ok(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById(int id)
    {
        var result = _doctorService.GetById(id);
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody]CreateDoctorRequest request)
    {
        var result = _doctorService.Add(request);
        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody] UpdateDoctorRequest request)
    {
        var result = _doctorService.Update(request);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        var result = _doctorService.Delete(id);
        return Ok(result);
    }
}