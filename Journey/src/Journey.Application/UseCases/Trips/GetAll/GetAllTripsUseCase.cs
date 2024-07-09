using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public static class GetAllTripsUseCase
    {
        public static ResponseTripsJson Execute()
        {
            var dbContext = new JorneyDbContext();
            var trips = dbContext.Trips.ToList();

            return new ResponseTripsJson 
            {
                Trips = trips.Select(t => new ResponseShortTripJson 
                {
                    Id = t.Id,
                    Name = t.Name,
                    EndDate = t.EndDate,
                    StartDate = t.StartDate
                }).ToList()
            };
        }
    }
}
