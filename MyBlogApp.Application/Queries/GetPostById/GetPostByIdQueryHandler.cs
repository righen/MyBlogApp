using MediatR;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Application.Queries.GetPostById;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        return await _postRepository.GetByIdAsync(request.Id);
    }
}