using MediatR;
using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Application.Queries.GetAllPosts;

public class GetAllPostsQuery : IRequest<IEnumerable<Post>>
{
}