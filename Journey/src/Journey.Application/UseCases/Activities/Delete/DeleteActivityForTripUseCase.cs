using Journey.Infrastructure;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Delete
{
    public static class DeleteActivityForTripUseCase
    {
        public static void Execute(Guid tripId, Guid actvityId)
        {
            var dbContext = new JorneyDbContext();
            var activity = dbContext.Activities.FirstOrDefault(a => a.Id == actvityId && a.TripId == tripId);

            if (activity is null)
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            dbContext.Activities.Remove(activity);
            dbContext.SaveChanges();
        }
    }
}
