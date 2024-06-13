using MediatR;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.ValueObjects;

namespace MyBlogApp.Application.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null) throw new KeyNotFoundException("Post not found");

        var title = Title.Create(request.Title);
        var content = Content.Create(request.Content);

        post.Update(title, content);

        await _postRepository.UpdateAsync(post);

        return Unit.Value;
    }
}