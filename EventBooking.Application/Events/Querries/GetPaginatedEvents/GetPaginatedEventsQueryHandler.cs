using System;
using System.Linq.Expressions;
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
        Expression<Func<Event, bool>> predicate = x => true;
        var res = await _repository.GetPaginatedAsync(predicate, request.PageIndex, request.PageSize);
        return _mapper.Map<PaginatedList<EventDto>>(res);

    }
}


