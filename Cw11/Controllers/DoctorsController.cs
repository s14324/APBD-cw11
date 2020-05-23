using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw11.DTOs.Requests;
using Cw11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _service;

        public DoctorsController(IDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_service.GetDoctors());
        }

        [HttpPost]
        public IActionResult AddDoctor(AddDoctorRequest request)
        {
            return Ok(_service.AddDoctor(request));
        }

        [HttpPut]
        public IActionResult UpdateDoctor(UpdateDoctorRequest request)
        {
            return Ok(_service.UpdateDoctor(request));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            return Ok(_service.DeleteDoctor(id));
        }
    }
}