using BarberBoss.Application.Services.Register;
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
        public async Task<IActionResult> Register(
            [FromServices] IRegisterServiceUseCase useCase,
            [FromBody] RequestRegisterServiceJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
