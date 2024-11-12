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
        public IActionResult Register([FromBody] RequestRegisterServiceJson request)
        {
            try
            {
                var useCase = new RegisterServiceUseCase();

                var result = useCase.Execute(request);

                return Created(string.Empty, result);
            }
            catch (ErrorOnValidationException ex)
            {
                var errorResponse = new ResponseErrorJson(ex.Errors);

                return BadRequest(errorResponse);
            }
            catch
            {
                var errorResponse = new ResponseErrorJson("Unknown error");

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
