using EventBooking.Application.Common.Interfaces.Services;
using EventBooking.Application.Events.Commands.CreateEvent;
using EventBooking.Application.Events.Commands.DeleteEvent;
using EventBooking.Application.Events.Commands.UpdateEvent;
using EventBooking.Application.Events.Querries.GetAllEvents;
using EventBooking.Application.Events.Querries.GetEventById;
using EventBooking.Application.Events.Querries.GetPaginatedEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventBooking.Api.Controllers;


public class EventsController : ApiBaseController
{
    private readonly ISender _mediator;
    private readonly IUserService _userService;
    public EventsController(ISender mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventCommand createEventCommand)
    {
        var res = await _mediator.Send(createEventCommand);
        return res.Match(evnt => Ok(evnt), errors => Problem(errors));
    }

    [HttpGet("")]
    public async Task<IActionResult> GetEventsAsync()
    {
        var res = await _mediator.Send(new GetAllEventsQuery());
        return res.Match(evnt => Ok(evnt), errors => Problem(errors));
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedEventsAsync([FromQuery] GetPaginatedEventsQuery getPaginatedEventsQuery)
    {
        var res = await _mediator.Send(getPaginatedEventsQuery);
        return res.Match(evnt => Ok(evnt), errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventByIdAsync(Guid id)
    {
        var res = await _mediator.Send(new GetEventByIdQuery(id));
        return res.Match(evnt => Ok(evnt), errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEventsAsync(Guid id, [FromBody] UpdateEventCommand updateEventCommand)
    {
        if (id != updateEventCommand.Id)
        {
            return Problem("Invalid request");
        }
        var res = await _mediator.Send(updateEventCommand);
        return res.Match(evnt => Ok(evnt), errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventsAsync(Guid id)
    {
        var res = await _mediator.Send(new DeleteEventCommand(id));
        return res.Match(evnt => Ok(evnt), errors => Problem(errors));
    }
}


