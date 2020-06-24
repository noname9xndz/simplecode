using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProCodeGuide.Samples.Automapper.DTO;
using ProCodeGuide.Samples.Automapper.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProCodeGuide.Samples.Automapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeDTO _employeeDTO)
        {
            var employeeModel = _mapper.Map<EmployeeModel>(_employeeDTO);
            return Ok(employeeModel);
        }
    }
}
