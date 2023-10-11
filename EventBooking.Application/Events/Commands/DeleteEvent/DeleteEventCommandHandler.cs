using System;
using ErrorOr;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Common.Errors;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events;
using MapsterMapper;
using MediatR;

namespace EventBooking.Application.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, ErrorOr<EventDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Event> _eventRepository;
    private readonly IMapper _mapper;
    public DeleteEventCommandHandler(IRepository<Event> eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<EventDto>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var evnt = await _eventRepository.GetByIdAsync(request.Id);
        if (evnt is null)
        {
            return EventErrors.EventNotFound;
        }
        await _eventRepository.DeleteAsync(evnt);
        await _unitOfWork.Save();
        return _mapper.Map<EventDto>(evnt);
    }
}


