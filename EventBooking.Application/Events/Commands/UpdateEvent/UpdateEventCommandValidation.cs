
using FluentValidation;

namespace EventBooking.Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidation : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("id is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("description is required");
            RuleFor(x => x.Address.Country).NotEmpty().WithMessage("country is required");
            RuleFor(x => x.Address.Number).NotEmpty().WithMessage("house number is required");
            RuleFor(x => x.Address.State).NotEmpty().WithMessage("state is required");
            RuleFor(x => x.Address.Street).NotEmpty().WithMessage("street is required");
            RuleFor(x => x.Duration.EndDate).NotEmpty().WithMessage("event end date is required");
            RuleFor(x => x.Duration.StartDate).NotEmpty().WithMessage("event start date is required");
            RuleFor(x => x.Duration.TimeZone).NotEmpty().WithMessage("time zone is required");
        }
    }
}

