using MediatR;
using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Application.Queries.GetPostById;

public class GetPostByIdQuery : IRequest<Post>
{
    public GetPostByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}