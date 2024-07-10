using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Complete
{
    public static class CompleteActivityForTripUseCase
    {
        public static void Execute(Guid tripId, Guid activityId)
        {
            var dbContext = new JorneyDbContext();
            var activity = dbContext.Activities.FirstOrDefault(a => a.Id == activityId && a.TripId == tripId);

            if (activity is null)
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            activity.Status = Infrastructure.Enums.ActivityStatus.Done;
            dbContext.Activities.Update(activity);
            dbContext.SaveChanges();


        }
    }
}
