using System;
using ErrorOr;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Common.Models.Users;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events;
using EventBooking.Domain.Events.ValueObjects;
using MapsterMapper;
using MediatR;

namespace EventBooking.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ErrorOr<EventDto>>
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventCommandHandler(IRepository<Event> eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = Event.Create(request.Title, request.Description, EventDuration.Create(request.Duration.StartDate, request.Duration.EndDate, request.Duration.TimeZone), Address.Create(request.Address.Street, request.Address.State, request.Address.Country, request.Address.Number), "1", DateTime.UtcNow);
            await _eventRepository.AddAsync(newEvent);
            await _unitOfWork.Save();
            var eventRes = _mapper.Map<EventDto>(newEvent);
            return eventRes;
        }
    }
}

