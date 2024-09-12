namespace Tidjma.Repository;
using Microsoft.EntityFrameworkCore;
using Tidjma.Helpers;
using Tidjma.Models;
using Tidjma.Contracts;
using Tidjma.Data;

/*
 *
 * Fix Error Handling here
 * 
 * */
public class CommentRepository
{
    private readonly TidjmaDbContext _dbContext;
    private readonly ILogger<CommentRepository> _logger;
    private readonly BlogMapper _mapper;

    public CommentRepository(
            BlogMapper mapper,
            TidjmaDbContext DbC,
            ILogger<CommentRepository> logger
           )
    {
        _dbContext = DbC;
        _logger = logger;
        _mapper = mapper;
    }


    // this is used in /articles/{article_id}/comments/
    public IEnumerable<CommentDTO>? ListByArticleId(int articleId) 
    { 
        return  _dbContext
            .Comments
            .ToList()
            .Where(e => e.ArticleId == articleId)
            .Select(e => _mapper.CommentToCommentDTO(e));
    }

    // this is used in /users/{user_id}/comments/
    public IEnumerable<CommentDTO>? ListByAuthorId(int authorId) 
    { 
            return _dbContext
                .Comments
                .ToList()
                .Where(e => e.AuthorId == authorId)
                .Select(e => _mapper.CommentToCommentDTO(e));
    }

    public bool Create(CreateCommentDTO newComment)
    {
        var user = _dbContext.Users.Where(e => e.Id == newComment.AuthorId).FirstOrDefault();
        var article = _dbContext.Articles.Where(e => e.Id == newComment.ArticleId).FirstOrDefault();

        if (user is not null && article is not null)
        {
            Comment comment = new()
            {
                Article = article,
                User = user,
                Body = newComment.Body
            }; // mapping

            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Update(UpdateCommentDTO updatedComment)
    {
        var user = _dbContext.Users.Where(e => e.Id == updatedComment.AuthorId).FirstOrDefault();
        var article = _dbContext.Articles.Where(e => e.Id == updatedComment.ArticleId).FirstOrDefault();

        if (user is not null && article is not null)
        {

            _dbContext.Update(_mapper.UpdateCommentDTOToComment(updatedComment));
            _dbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Delete(int articleId, int commentId)
    {
        var comment = _dbContext.Comments.Where(
                e => e.ArticleId == articleId &&
                e.Id == commentId
                ).FirstOrDefault();

        if (comment is not null)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();

            return true;
        }

        return false;
    }
}
