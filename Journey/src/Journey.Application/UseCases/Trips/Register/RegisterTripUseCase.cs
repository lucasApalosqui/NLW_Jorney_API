using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Journey.Communication.Responses;

namespace Journey.Application.UseCases.Trips.Register
{
    public static class RegisterTripUseCase
    {
        public static ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);
            var dbContext = new JorneyDbContext();
            var entity = new Trip 
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            dbContext.Trips.Add(entity);

            dbContext.SaveChanges();

            return new ResponseShortTripJson 
            {
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Id = entity.Id
            };
        }

        private static void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
                
        } 
    }
}
