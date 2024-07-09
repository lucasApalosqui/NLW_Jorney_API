using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Trips.Delete
{
    public static class DeleteTripByIdUseCase
    {
        public static void Execute(Guid id)
        {
            var jorneyContext = new JorneyDbContext();
            var trip = jorneyContext
                .Trips
                .FirstOrDefault(x => x.Id == id);

            if (trip == null)
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            jorneyContext.Trips.Remove(trip);

            jorneyContext.SaveChanges();
        }
    }
}
