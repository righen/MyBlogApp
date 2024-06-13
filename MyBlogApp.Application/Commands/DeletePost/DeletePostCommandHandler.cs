using MediatR;
using MyBlogApp.Application.Interfaces;

namespace MyBlogApp.Application.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null) throw new KeyNotFoundException("Post not found");

        await _postRepository.DeleteAsync(request.Id);

        return Unit.Value;
    }
}