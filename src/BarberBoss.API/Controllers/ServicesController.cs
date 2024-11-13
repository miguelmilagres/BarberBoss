﻿using BarberBoss.Application.Services.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterServiceJson request)
        {
            var useCase = new RegisterServiceUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
