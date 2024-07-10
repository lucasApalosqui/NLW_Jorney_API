using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Register;
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
       [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
       [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]

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
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [Route("{tripId}/activity")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity([FromRoute]Guid tripId, [FromBody] RequestRegisterActivityJson request)
        {
            var response = RegisterActivityForTripUseCase.Execute(tripId, request);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{tripId}/activity/{activityId}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult CompleteActivity([FromRoute] Guid activityId, [FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
        {
            CompleteActivityForTripUseCase.Execute(tripId, activityId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{tripId}/activity/{activityId}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult DeleteActivity([FromRoute] Guid activityId, [FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
        {
            DeleteActivityForTripUseCase.Execute(tripId, activityId);
            return NoContent();
        }
    }
}
