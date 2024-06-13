using FluentValidation;
using MyBlogApp.Application.Commands.CreatePost;

namespace MyBlogApp.Application.Validators;

public class UpdatePostCommandValidator :  AbstractValidator<CreatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
    }
}