using System;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Events.Commands.DeleteEvent;
using EventBooking.Application.Events.Commands.UpdateEvent;
using EventBooking.Application.UnitTest.Events.Commands.DeleteEvent.TestUtils;
using EventBooking.Application.UnitTest.Events.Commands.UpdateEvent.TestUtils;
using EventBooking.Application.UnitTest.TestUtils.Models;
using EventBooking.Domain.Events;
using FluentAssertions;
using MapsterMapper;
using Moq;

namespace EventBooking.Application.UnitTest.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandlerTest
{
    private readonly DeleteEventCommandHandler _handler;
    private readonly Mock<IRepository<Event>> _mockEventRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    public DeleteEventCommandHandlerTest()
    {
        _mockEventRepository = new Mock<IRepository<Event>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _handler = new DeleteEventCommandHandler(_mockEventRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task HandleDeleteEventCommandHandler_WhenEventIdIsValid_ShouldDeleteAndReturnEvent()
    {
        //Arrange
        var deleteEventCommand = DeleteEventCommandUtils.DeleteCommand();
        var evnt = EventUtils.CreateEvent();

        _mockEventRepository
        .Setup(repo => repo.GetByIdAsync(deleteEventCommand.Id))
        .ReturnsAsync(evnt);

        _mockEventRepository
        .Setup(repo => repo.DeleteAsync(evnt))
        .Returns(Task.CompletedTask);

        _mockUnitOfWork
        .Setup(uow => uow.Save())
        .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(deleteEventCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        _mockEventRepository.Verify(m => m.DeleteAsync(evnt), Times.Once);
        _mockEventRepository.Verify(m => m.GetByIdAsync(deleteEventCommand.Id), Times.Once);
    }

    [Fact]
    public async Task HandleDeleteEventCommandHandler_WhenEventIdIsInValid_ShouldReturnEventNotFound()
    {
        //Arrange
        var deleteEventCommand = DeleteEventCommandUtils.DeleteCommand();
        var evnt = EventUtils.CreateEvent();

        _mockEventRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Event>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(deleteEventCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        _mockEventRepository.Verify(m => m.DeleteAsync(evnt), Times.Never);
        _mockEventRepository.Verify(m => m.GetByIdAsync(deleteEventCommand.Id), Times.Once);
    }

}

