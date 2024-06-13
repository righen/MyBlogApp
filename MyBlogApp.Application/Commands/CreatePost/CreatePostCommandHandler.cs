using MediatR;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.Entities;
using MyBlogApp.Domain.ValueObjects;

namespace MyBlogApp.Application.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var title = Title.Create(request.Title);
        var content = Content.Create(request.Content);

        var post = new Post(title, content);

        await _postRepository.AddAsync(post);

        return post.Id;
    }
}