using System;
using EventBooking.Application.Common.Models.Users;
using EventBooking.Application.Events.Commands.UpdateEvent;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Common.Helpers;
using EventBooking.Domain.Events;
using Mapster;

namespace EventBooking.Infrastructure.Common.Mappings;

public class EventMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Event Entity, UserModel User), EventResponseModel>()
            .Map(dest => dest.User, src => src.User)
            .Map(dest => dest, src => src.Entity);
        config.NewConfig<PaginatedList<Event>, PaginatedList<EventDto>>();
        config.NewConfig<Event, EventDto>();
        config.NewConfig<UpdateEventCommand, Event>();

    }
}


