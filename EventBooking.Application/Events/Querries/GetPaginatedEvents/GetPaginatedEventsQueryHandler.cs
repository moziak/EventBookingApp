using System;
using System.Linq.Expressions;
using Application.Extensions;
using ErrorOr;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Domain.Common.Helpers;
using EventBooking.Domain.Events;
using Mapster;
using MapsterMapper;
using MediatR;

namespace EventBooking.Application.Events.Querries.GetPaginatedEvents;

public class GetPaginatedEventsQueryHandler : IRequestHandler<GetPaginatedEventsQuery, ErrorOr<PaginatedList<EventDto>>>
{
    private readonly IRepository<Event> _repository;
    private readonly IMapper _mapper;
    public GetPaginatedEventsQueryHandler(IRepository<Event> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<PaginatedList<EventDto>>> Handle(GetPaginatedEventsQuery request, CancellationToken cancellationToken)
    {
        //TODO Handle filters with Specification pattern
        Expression<Func<Event, bool>> predicate = x => true;
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            predicate = predicate.And(x => x.Title.ToLower() == request.Title.ToLower());
        }
        if (!string.IsNullOrWhiteSpace(request.State))
        {
            predicate = predicate.And(x => x.Address.State.ToLower() == request.State.ToLower());
        }
        if (!string.IsNullOrWhiteSpace(request.Country))
        {
            predicate = predicate.And(x => x.Address.Country.ToLower() == request.Country.ToLower());
        }
        if (!string.IsNullOrWhiteSpace(request.TimeZone))
        {
            predicate = predicate.And(x => x.Duration.TimeZone.ToLower() == request.TimeZone.ToLower());
        }
        if (request.EventStartDate.HasValue)
        {
            predicate = predicate.And(x => x.Duration.StartDate >= request.EventStartDate);
        }
        if (request.EventEndDate.HasValue)
        {
            predicate = predicate.And(x => x.Duration.EndDate <= request.EventStartDate);
        }
        var res = await _repository.GetPaginatedAsync(predicate, request.PageIndex, request.PageSize);
        return _mapper.Map<PaginatedList<EventDto>>(res);

    }
}


