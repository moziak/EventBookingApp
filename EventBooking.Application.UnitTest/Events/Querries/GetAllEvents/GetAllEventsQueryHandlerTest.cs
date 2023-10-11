using System;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Common.Models;
using EventBooking.Application.Events.Querries.GetAllEvents;
using EventBooking.Application.Events.Querries.GetEventById;
using EventBooking.Application.UnitTest.TestUtils.Models;
using EventBooking.Domain.Events;
using FluentAssertions;
using MapsterMapper;
using Moq;

namespace EventBooking.Application.UnitTest.Events.Querries.GetAllEvents;

public class GetAllEventsQueryHandlerTest
{
    private readonly GetAllEventsQueryHandler _handler;
    private readonly Mock<IRepository<Event>> _mockEventRepository;
    private readonly Mock<IMapper> _mockMapper;
    public GetAllEventsQueryHandlerTest()
    {
        _mockEventRepository = new Mock<IRepository<Event>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetAllEventsQueryHandler(_mockEventRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task HandleGetAllEventsQueryHandler_WhenEvent_ShouldReturnListOfEvent()
    {


        //Arrange
        var getAllEventsQuery = new GetAllEventsQuery();
        var evnts = new List<Event>
        {
            EventUtils.CreateEvent()
        };
        var evtResponse = new List<EventDto> { EventDtoUtils.CreateEventDto() };

        _mockEventRepository
        .Setup(repo => repo.GetAllAsync())
        .ReturnsAsync(evnts);

        _mockMapper
            .Setup(mapper => mapper.Map<List<EventDto>>(It.IsAny<List<Event>>()))
            .Returns(evtResponse);

        // Act
        var result = await _handler.Handle(getAllEventsQuery, default);
        var response = _mockMapper.Object.Map<List<EventDto>>(result.Value);
        // Assert
        result.IsError.Should().BeFalse();
        //response.Should().HaveCount(1);
        //response.Should().Contain(item => item.Id == EventDtoUtils.CreateEventDto().Id);
        _mockEventRepository.Verify(m => m.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleGetAllEventsQueryHandler_WhenEventIsEmpty_ShouldReturnEmptyList()
    {


        //Arrange
        var getAllEventsQuery = new GetAllEventsQuery();
        var evnts = new List<Event>();
        var evtResponse = new List<EventDto> { EventDtoUtils.CreateEventDto() };

        _mockEventRepository
        .Setup(repo => repo.GetAllAsync())
        .ReturnsAsync(evnts);

        // Act
        var result = await _handler.Handle(getAllEventsQuery, default);

        // Assert
        result.IsError.Should().BeFalse();
        _mockEventRepository.Verify(m => m.GetAllAsync(), Times.Once);
    }

}

