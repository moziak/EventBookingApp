using FluentValidation;

namespace EventBooking.Application.Events.Commands.DeleteEvent;

public class DeleteEventCommandValidation : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("event id is required");
    }
}

