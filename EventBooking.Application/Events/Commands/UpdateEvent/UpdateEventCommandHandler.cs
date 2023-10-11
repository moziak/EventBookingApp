using System;
using ErrorOr;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Common.Errors;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events;
using MapsterMapper;
using MediatR;

namespace EventBooking.Application.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, ErrorOr<EventDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Event> _eventRepository;
    private readonly IMapper _mapper;
    public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IRepository<Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<EventDto>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var evnt = await _eventRepository.GetByIdAsync(request.Id);
        if (evnt is null)
        {
            return EventErrors.EventNotFound;
        }
        evnt.Address.Update(request.Address.Street, request.Address.State, request.Address.Country, request.Address.Number);
        evnt.Duration.Update(request.Duration.StartDate, request.Duration.EndDate, request.Duration.TimeZone);
        evnt.Update(request.Title, request.Description);
        await _eventRepository.UpdateAsync(evnt);
        await _unitOfWork.Save();
        return _mapper.Map<EventDto>(evnt);

    }
}


