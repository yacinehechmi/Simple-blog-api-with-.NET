namespace Tidjma.Helpers;
using FluentValidation;
using Tidjma.Contracts;

// users
public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Username).NotNull().Length(4, 40);
        RuleFor(user => user.Email).NotNull().EmailAddress();
        RuleFor(user => user.Password).NotNull().Length(50, 100);
    }
}

public class UserValidator : AbstractValidator<CreateUserDTO>
{
    public UserValidator()
    {
        RuleFor(user => user.Username).NotNull().Length(4, 40);
        RuleFor(user => user.Email).NotNull().EmailAddress();
        RuleFor(user => user.Password).NotNull().Length(50, 100);
    }
}

// comments
public class CommentValidator : AbstractValidator<CreateCommentDTO>
{
    public CommentValidator()
    {
        RuleFor(comment => comment.AuthorId).NotEmpty();
        RuleFor(comment => comment.ArticleId).NotEmpty();
        RuleFor(comment => comment.Body).NotEmpty();
    }
}

public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTO>
{
    public UpdateCommentValidator()
    {
        RuleFor(comment => comment.Id).NotEmpty();
        RuleFor(comment => comment.AuthorId).NotEmpty();
        RuleFor(comment => comment.ArticleId).NotEmpty();
        RuleFor(comment => comment.Body).NotNull();
    }
}

// articles
public class ArticleValidator : AbstractValidator<CreateArticleDTO>
{
    public ArticleValidator()
    {
        RuleFor(article => article.AuthorId).NotEmpty();
        RuleFor(article => article.Title).NotEmpty();
        RuleFor(article => article.Body).NotEmpty().MinimumLength(10);
        RuleFor(article => article.Release).NotEmpty();
    }
}

public class UpdateArticleValidator : AbstractValidator<UpdateArticleDTO>
{
    public UpdateArticleValidator()
    {
        RuleFor(article => article.AuthorId).NotEmpty();
        RuleFor(article => article.Title).NotEmpty();
        RuleFor(article => article.Body).NotNull();
    }
}


// make it so you could use only this to validate all
public class GenericValidator<T> : AbstractValidator<T>
{
    public GenericValidator()
    {
    }
}
