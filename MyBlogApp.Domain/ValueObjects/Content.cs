namespace MyBlogApp.Domain.ValueObjects;

public class Content
{
    private Content(string value)
    {
        if (value.Length > 1000)
            throw new ArgumentException("Content length exceeds limit", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public static Content Create(string value)
    {
        return new Content(value);
    }
}