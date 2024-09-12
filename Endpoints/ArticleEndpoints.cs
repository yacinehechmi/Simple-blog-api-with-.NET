namespace Tidjma.Endpoints;
using Tidjma.Repository;
using Tidjma.Helpers;
using Tidjma.Contracts;
using Microsoft.AspNetCore.Mvc;

public class ArticleEndpoint : ITJEndpoint
{
    public ILogger<ArticleEndpoint> _logger;
    public ArticleEndpoint( ILogger<ArticleEndpoint> logger)
    {
        _logger = logger;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        const string identifier = "articles";
        RouteGroupBuilder? articles =
            app.MapGroup("articles");

        // endpoint /articles 
        articles.MapGet("/", // GET /articles/
                ([FromServices] ArticleRepository reposistory) => {
                    OperationResult<IEnumerable<ArticleDTO>> result = reposistory.List();
                    return result.IsSuccess ? Results.Ok(result.Payload) : Results.BadRequest("bad");
                }).WithName(identifier); 

        articles.MapGet("/{articleId}", // GET /articles/id/
                ([FromRoute] int articleId,
                 [FromServices] ArticleRepository reposistory) => {
                OperationResult<ArticleDTO> result = reposistory.Get(articleId);

                return result.IsSuccess ? Results.Ok(result.Payload) : Results.NotFound("bad");
                });

        articles.MapPost("/", // POST /articles/{articleId}
                ([FromBody] CreateArticleDTO newArticle,
                 [FromServices] ArticleRepository repository,
                 [FromServices] ArticleValidator validator) => {

                var validation = validator.Validate(newArticle);
                if (validation.IsValid)
                {
                    OperationResult<object> result = repository.Create(newArticle);
                    return result.IsSuccess ? Results.Created() : Results.BadRequest("bad");
                }

                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
                });

        articles.MapPut("/{id}", // PUT /articles
                ([FromRoute] int id,
                 [FromBody] UpdateArticleDTO updatedArticle,
                 [FromServices] ArticleRepository repository,
                 [FromServices] UpdateArticleValidator validator) => {
                if (id != updatedArticle.Id)
                {
                    return Results.BadRequest("id's are not identical");
                }

                var validation = validator.Validate(updatedArticle);
                if (validation.IsValid)
                {
                    OperationResult<object> operationResult = repository.Update(updatedArticle, id);
                    if (operationResult.IsSuccess)
                    {
                        return Results.Created();
                    }
                    else
                    {
                        _logger.LogInformation("--------------------------- LOGGER ------------------------------");
                        _logger.LogInformation(operationResult.ExceptionType.Message);
                        return Results.BadRequest("OMG");
                    }
                }

                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
                });

        articles.MapDelete("/{id}", 
                ([FromServices] ArticleRepository repository,
                 [FromRoute] int id) => {

                OperationResult<object> result = repository.Delete(id);
                return result.IsSuccess ? Results.Ok("asset deleted") : Results.NotFound("could not find asset");
                });
        
        // articles/comments/
        articles.MapGet("/{articleId}/comments", // GET /articles/id/comments/
                ([FromRoute] int articleId,
                 [FromServices] CommentRepository repository) => {
                    return Results.Ok(repository.ListByArticleId(articleId));
                });

        articles.MapPost("/{articleId}/comments/", // POST /articles/{articleId}/comments/
                ([FromBody] CreateCommentDTO newComment,
                 int articleId,
                 [FromServices] CommentRepository repository,
                 [FromServices] CommentValidator validator) => {

                if (articleId != newComment.ArticleId)
                {
                    return Results.BadRequest("id do not match with body");
                }

                var validation = validator.Validate(newComment);
                
                if (validation.IsValid)
                {
                    return repository.Create(newComment) ? Results.Created() : Results.BadRequest("something went wrong");
                }

                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
                });

        articles.MapPut("/{articleId}/comments/", // put /articles/{articleId}/comments/
                ([FromBody] UpdateCommentDTO updatedComment,
                 [FromRoute] int articleId,
                 [FromServices] CommentRepository repository,
                 [FromServices] UpdateCommentValidator validator) => {
                if (articleId != updatedComment.ArticleId)
                {
                    return Results.BadRequest("id do not match with body");
                }

                var validation = validator.Validate(updatedComment);

                if (validation.IsValid)
                {
                    return repository.Update(updatedComment) ? Results.Created() : Results.BadRequest("something went wrong");
                }

                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
                });

        articles.MapDelete("/{articleId}/comments/{commentId}", // DELETE /articles/{articleId}/comments/
                ([FromRoute] int articleId,
                 [FromRoute] int commentId,
                 [FromServices] CommentRepository repository) => {

                return repository.Delete(articleId, commentId) ? Results.Ok("Deleted") : Results.NotFound("Could not find ressource");
                });
    }
}
