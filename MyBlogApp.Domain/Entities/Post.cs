using MyBlogApp.Domain.ValueObjects;

namespace MyBlogApp.Domain.Entities;

public class Post
{
    private Post()
    {
    }

    public Post(Title title, Content content)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; }
    public Title Title { get; private set; }
    public Content Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void Update(Title title, Content content)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }
}