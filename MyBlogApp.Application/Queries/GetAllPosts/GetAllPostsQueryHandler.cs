using MediatR;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Application.Queries.GetAllPosts;

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetAllPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        return await _postRepository.GetAllAsync();
    }
}