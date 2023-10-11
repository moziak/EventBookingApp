using System;
using ErrorOr;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events;
using MapsterMapper;
using MediatR;

namespace EventBooking.Application.Events.Querries.GetAllEvents;

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, ErrorOr<List<EventDto>>>
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetAllEventsQueryHandler(IRepository<Event> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var evnts = await _eventRepository.GetAllAsync();
        return _mapper.Map<List<EventDto>>(evnts);


    }
}

