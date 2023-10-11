using System;
using ErrorOr;
using EventBooking.Domain.Common;
using EventBooking.Application.Common.Interfaces.Interfaces;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Common.Interfaces.Services;
using EventBooking.Application.Common.Models.Users;
using EventBooking.Application.Events.Common.Errors;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Events;
using MapsterMapper;
using MediatR;

namespace EventBooking.Application.Events.Querries.GetEventById;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, ErrorOr<EventResponseModel>>
{
    private readonly ICacheService _cacheService;
    private readonly IRepository<Event> _eventRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public GetEventByIdQueryHandler(ICacheService cacheService, IUserService userService, IRepository<Event> eventRepository, IMapper mapper)
    {
        _cacheService = cacheService;
        _eventRepository = eventRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<EventResponseModel>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var evnt = await _eventRepository.GetByIdAsync(request.Id);
        if (evnt is null)
        {
            return EventErrors.EventNotFound;
        }
        UserModel userInfo = await _cacheService.GetData<UserModel>("1");
        if (userInfo is null)
        {
            var userResponse = await _userService.GetUserByIdAsync(1);
            if (userResponse.IsError)
            {
                return UserErrors.UserNotFound;
            }
            userInfo = userResponse.Value;
            await _cacheService.SetData<UserModel>("1", userInfo, DateTimeOffset.UtcNow.AddMinutes(5));

        }
        var eventRes = _mapper.Map<EventResponseModel>((evnt, userInfo));
        return eventRes;


    }
}


