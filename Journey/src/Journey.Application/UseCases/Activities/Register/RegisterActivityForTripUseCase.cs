using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure.Entities;
using FluentValidation.Results;

namespace Journey.Application.UseCases.Activities.Register
{
    public static class RegisterActivityForTripUseCase
    {
        public static ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbContext = new JorneyDbContext();
            var trip = dbContext
                        .Trips
                        .Include(t => t.Activities)
                        .FirstOrDefault(t => t.Id == tripId);

            if (trip is null)
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            Validate(trip, request);
            var entity = new Activity
            {
                Name = request.Name,
                Date = request.Date,
                TripId = tripId
            };

            trip.Activities.Add(entity);
            dbContext.Activities.Add(entity);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Date = entity.Date,
                Name = entity.Name,
                Status = (Communication.Enums.ActivityStatus)entity.Status,
                Id = entity.Id
            };
        }

        private static void Validate(Trip? trip, RequestRegisterActivityJson request)
        {
            if(trip is null)
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            var validator = new RegisterActivityValidator();
            var result = validator.Validate(request);

            if (!(request.Date >= trip.StartDate && request.Date <= trip.EndDate))
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
