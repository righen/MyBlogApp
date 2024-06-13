using System.Text.Json.Serialization;
using MediatR;

namespace MyBlogApp.Application.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<Unit>
{
    [JsonIgnore] public int Id { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
}