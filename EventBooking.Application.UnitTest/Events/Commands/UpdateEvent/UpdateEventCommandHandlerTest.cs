using System;
using EventBooking.Application.UnitTest.Events.Commands.TestUtils;
using Moq;
using System.Reflection.Metadata;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Commands.CreateEvent;
using MapsterMapper;
using EventBooking.Domain.Events;
using EventBooking.Application.Events.Commands.UpdateEvent;
using EventBooking.Application.UnitTest.Events.Commands.UpdateEvent.TestUtils;
using FluentAssertions;
using EventBooking.Application.UnitTest.TestUtils.Models;

namespace EventBooking.Application.UnitTest.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandlerTest
{
    private readonly UpdateEventCommandHandler _handler;
    private readonly Mock<IRepository<Event>> _mockEventRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    public UpdateEventCommandHandlerTest()
    {
        _mockEventRepository = new Mock<IRepository<Event>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _handler = new UpdateEventCommandHandler(_mockUnitOfWork.Object, _mockEventRepository.Object, _mockMapper.Object);
    }


    [Fact]
    public async Task HandleUpdateEventCommandHandler_WhenEventIsValid_ShouldUpdateAndReturnEvent()
    {


        //Arrange
        var UpdateEventCommand = UpdateEventCommandUtils.UpdateCommand();
        var evnt = EventUtils.CreateEvent();

        _mockEventRepository
        .Setup(repo => repo.GetByIdAsync(UpdateEventCommand.Id))
        .Returns(Task.FromResult(evnt));

        _mockEventRepository
        .Setup(repo => repo.UpdateAsync(evnt))
        .Returns(Task.FromResult(evnt));

        _mockUnitOfWork
        .Setup(uow => uow.Save())
        .ReturnsAsync(1);

        var result = await _handler.Handle(UpdateEventCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        _mockEventRepository.Verify(m => m.UpdateAsync(evnt), Times.Once);
        _mockEventRepository.Verify(m => m.GetByIdAsync(UpdateEventCommand.Id), Times.Once);
    }


    [Fact]
    public async Task HandleUpdateEventCommandHandler_WhenEventIdIsInValid_ShouldReturnEventNotFound()
    {


        //Arrange
        var UpdateEventCommand = UpdateEventCommandUtils.UpdateCommand();
        var evnt = EventUtils.CreateEvent();

        _mockEventRepository
        .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
        .ReturnsAsync((Guid id) => null);

        // Act
        var result = await _handler.Handle(UpdateEventCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.FirstOrDefault().Description.Should().Be("Event not found");
        result.Errors.FirstOrDefault().Code.Should().Be("Event.NotFound");
        _mockEventRepository.Verify(m => m.GetByIdAsync(UpdateEventCommand.Id), Times.Once);
        _mockEventRepository.Verify(m => m.UpdateAsync(evnt), Times.Never);
    }
}

