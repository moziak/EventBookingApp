using System;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Commands.CreateEvent;
using EventBooking.Application.UnitTest.Events.Commands.CreateEvent.TestUtils;
using EventBooking.Domain.Events;
using FluentAssertions;
using MapsterMapper;
using Moq;

namespace EventBooking.Application.UnitTest.Events.Commands.CreateEvent;


public class CreateEventCommandHandlerTest
{
    private readonly CreateEventCommandHandler _handler;
    private readonly Mock<IRepository<Event>> _mockEventRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    public CreateEventCommandHandlerTest()
    {
        _mockEventRepository = new Mock<IRepository<Event>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateEventCommandHandler(_mockEventRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task HandleCreateEventCommandHandler_WhenEventIsValid_ShouldCreateAndReturnEvent()
    {


        //Arrange
        var createEventCommand = CreateEventCommandUtils.CreateCommand();

        _mockEventRepository
        .Setup(repo => repo.AddAsync(It.IsAny<Event>()))
        .Returns(Task.CompletedTask);

        _mockUnitOfWork
        .Setup(uow => uow.Save())
        .ReturnsAsync(1);

        //Act
        var result = await _handler.Handle(createEventCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        _mockEventRepository.Verify(m => m.AddAsync(It.IsAny<Event>()), Times.Once);
    }
}

