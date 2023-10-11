using System;
using EventBooking.Application.UnitTest.Events.Commands.TestUtils;
using Moq;
using System.Reflection.Metadata;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using MapsterMapper;
using EventBooking.Domain.Events;
using EventBooking.Application.Events.Querries.GetEventById;
using EventBooking.Application.Common.Interfaces.Services;
using EventBooking.Application.Common.Interfaces.Interfaces;
using FluentAssertions;
using EventBooking.Application.UnitTest.TestUtils.Models;
using EventBooking.Application.Events.Common.Models;

namespace EventBooking.Application.UnitTest.Events.Querries.GetEventById;

public class GetEventByIdQueryHandlerTest
{
    private readonly GetEventByIdQueryHandler _handler;
    private readonly Mock<IRepository<Event>> _mockEventRepository;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<ICacheService> _mockCacheService;
    private readonly Mock<IMapper> _mockMapper;
    public GetEventByIdQueryHandlerTest()
    {
        _mockEventRepository = new Mock<IRepository<Event>>();
        _mockUserService = new Mock<IUserService>();
        _mockCacheService = new Mock<ICacheService>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetEventByIdQueryHandler(_mockCacheService.Object, _mockUserService.Object, _mockEventRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task HandleGetEventByIdQueryHandler_WhenEventIdIsValid_ShouldReturnEvent()
    {


        //Arrange
        var getEventByIdQuery = new GetEventByIdQuery(TestUtils.Constants.Constants.Event.Id);
        var evnt = EventUtils.CreateEvent();
        var evtResponse = EventResponseUtils.CreateEventResponseModel();

        _mockEventRepository
        .Setup(repo => repo.GetByIdAsync(TestUtils.Constants.Constants.Event.Id))
        .ReturnsAsync(evnt);

        _mockMapper
        .Setup(mapper => mapper.Map<EventResponseModel>(It.IsAny<Event>()))
        .Returns(evtResponse);


        // Act
        var result = await _handler.Handle(getEventByIdQuery, default);
        var res = _mockMapper.Object.Map<EventResponseModel>(result.Value);

        // Assert
        result.IsError.Should().BeFalse();
        res.Description.Should().Be(evnt.Description);
        _mockEventRepository.Verify(m => m.GetByIdAsync(TestUtils.Constants.Constants.Event.Id), Times.Once);
    }

    [Fact]
    public async Task HandleGetEventByIdQueryHandler_WhenEventIdIsInValid_ShouldReturnEventNotFound()
    {


        //Arrange
        var getEventByIdQuery = new GetEventByIdQuery(TestUtils.Constants.Constants.Event.Id);
        var evnt = EventUtils.CreateEvent();
        var evtResponse = EventResponseUtils.CreateEventResponseModel();

        _mockEventRepository
        .Setup(repo => repo.GetByIdAsync(TestUtils.Constants.Constants.Event.Id))
        .ReturnsAsync((Guid id) => null);


        // Act
        var result = await _handler.Handle(getEventByIdQuery, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.FirstOrDefault().Description.Should().Be("Event not found");
        result.Errors.FirstOrDefault().Code.Should().Be("Event.NotFound");
        _mockEventRepository.Verify(m => m.GetByIdAsync(TestUtils.Constants.Constants.Event.Id), Times.Once);
    }
}

