using FluentAssertions;
using Moq;
using MyBlogApp.Application.Commands.UpdatePost;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.Entities;
using MyBlogApp.Domain.ValueObjects;

namespace MyBlogApp.Tests.UnitTests;

public class UpdatePostCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdatePost_WhenValidRequest()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        var existingPost = new Post(Title.Create("Old Title"), Content.Create("Old Content"));
        mockRepository.Setup(repo => repo.GetByIdAsync(existingPost.Id)).ReturnsAsync(existingPost);

        var handler = new UpdatePostCommandHandler(mockRepository.Object);
        var command = new UpdatePostCommand
        {
            Id = existingPost.Id,
            Title = "Updated Title",
            Content = "Updated Content"
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(
            repo => repo.UpdateAsync(It.Is<Post>(p =>
                p.Id == existingPost.Id && p.Title.Value == command.Title && p.Content.Value == command.Content)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenPostNotFound()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

        var handler = new UpdatePostCommandHandler(mockRepository.Object);
        var command = new UpdatePostCommand
        {
            Id = 999, // Non-existent post ID
            Title = "Updated Title",
            Content = "Updated Content"
        };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Post not found");
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenTitleIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        var existingPost = new Post(Title.Create("Old Title"), Content.Create("Old Content"));
        mockRepository.Setup(repo => repo.GetByIdAsync(existingPost.Id)).ReturnsAsync(existingPost);

        var handler = new UpdatePostCommandHandler(mockRepository.Object);
        var command = new UpdatePostCommand
        {
            Id = existingPost.Id,
            Title = string.Empty, // Invalid input
            Content = "Updated Content"
        };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Title cannot be empty (Parameter 'value')");
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenContentIsTooLong()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        var existingPost = new Post(Title.Create("Old Title"), Content.Create("Old Content"));
        mockRepository.Setup(repo => repo.GetByIdAsync(existingPost.Id)).ReturnsAsync(existingPost);

        var handler = new UpdatePostCommandHandler(mockRepository.Object);
        var command = new UpdatePostCommand
        {
            Id = existingPost.Id,
            Title = "Valid Title",
            Content = new string('a', 1001) // Edge case: content length exceeds limit
        };

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Content length exceeds limit (Parameter 'value')");
    }
}