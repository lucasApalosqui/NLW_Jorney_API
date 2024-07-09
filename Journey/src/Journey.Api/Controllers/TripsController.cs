using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {

       [HttpPost]
       [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
       [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]

       public IActionResult Register([FromBody]RequestRegisterTripJson request)
        {
            var response = RegisterTripUseCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var response = GetAllTripsUseCase.Execute();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var response = GetTripByIdUseCase.Execute(id);

            return Ok(response);

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            DeleteTripByIdUseCase.Execute(id);

            return NoContent();
        }
    }
}
