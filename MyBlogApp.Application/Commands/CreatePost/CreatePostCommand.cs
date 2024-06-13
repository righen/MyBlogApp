using MediatR;

namespace MyBlogApp.Application.Commands.CreatePost;

public class CreatePostCommand : IRequest<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
}