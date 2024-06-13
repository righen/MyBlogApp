using MediatR;

namespace MyBlogApp.Application.Commands.DeletePost;

public class DeletePostCommand : IRequest<Unit>
{
    public DeletePostCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}