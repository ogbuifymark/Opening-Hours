using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpeningHoursApp.Models;
using OpeningHoursApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeningHoursApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly IOpeningHoursAppService _openingHoursAppService;

        public ScheduleController(IOpeningHoursAppService openingHoursAppService)
        {
            _openingHoursAppService = openingHoursAppService;
        }

        [HttpPost]
        public IActionResult ProcessSchedule(ScheduleRequestModel model)
        {

            string result = _openingHoursAppService.ProcessSchedule(model.SchedulePlan);
            return Ok(new { Message = result });

        }
    }
}
