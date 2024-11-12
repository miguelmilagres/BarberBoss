using BarberBoss.Application.Services.Register;
using BarberBoss.Communication.Requests;
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

            var result = useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
