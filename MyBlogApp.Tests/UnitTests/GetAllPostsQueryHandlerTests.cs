using FluentAssertions;
using Moq;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Application.Queries.GetAllPosts;
using MyBlogApp.Domain.Entities;
using MyBlogApp.Domain.ValueObjects;

namespace MyBlogApp.Tests.UnitTests;

public class GetAllPostsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAllPosts()
    {
        // Arrange
        var mockRepository = new Mock<IPostRepository>();
        var posts = new List<Post>
        {
            new(Title.Create("Title 1"), Content.Create("Content 1")),
            new(Title.Create("Title 2"), Content.Create("Content 2"))
        };
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(posts);

        var handler = new GetAllPostsQueryHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(new GetAllPostsQuery(), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(posts);
    }
}