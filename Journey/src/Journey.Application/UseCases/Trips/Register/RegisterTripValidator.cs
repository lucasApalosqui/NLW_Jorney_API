using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
            RuleFor(request => request.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATHER_THAN_TODAY);
            RuleFor(request => request)
                .Must(r => r.EndDate.Date > r.StartDate.Date)
                .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATHER_THAN_START_DATE);
        }
    }
}
