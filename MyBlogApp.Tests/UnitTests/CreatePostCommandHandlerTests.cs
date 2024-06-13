using FluentAssertions;
using Moq;
using MyBlogApp.Application.Commands.CreatePost;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Tests.UnitTests;

public class CreatePostCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreatePost_WhenValidRequest()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        var handler = new CreatePostCommandHandler(mockRepository.Object);
        var command = new CreatePostCommand
        {
            Title = "New Post",
            Content = "This is the content of the new post."
        };

        // Set up the mock repository to return a valid post ID when a post is added
        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Post>())).Callback<Post>(post => post.GetType()
            .GetProperty("Id")
            ?.SetValue(post, 1));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(
            repo => repo.AddAsync(
                It.Is<Post>(p => p.Title.Value == command.Title && p.Content.Value == command.Content)), Times.Once);
        result.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenTitleIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        var handler = new CreatePostCommandHandler(mockRepository.Object);
        var command = new CreatePostCommand
        {
            Title = string.Empty, // Invalid input
            Content = "This is the content of the new post."
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
        var handler = new CreatePostCommandHandler(mockRepository.Object);
        var command = new CreatePostCommand
        {
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