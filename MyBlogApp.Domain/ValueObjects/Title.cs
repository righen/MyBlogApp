namespace MyBlogApp.Domain.ValueObjects;

public class Title
{
    private Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public static Title Create(string value)
    {
        return new Title(value);
    }
}