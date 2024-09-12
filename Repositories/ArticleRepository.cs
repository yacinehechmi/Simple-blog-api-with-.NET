namespace Tidjma.Repository;
using Tidjma.Models;
using Tidjma.Contracts;
using Tidjma.Data;
using Tidjma.Helpers;

/*
 *
 * Fix Error Handling here
 * 
 * */
public class ArticleRepository
{
    private readonly TidjmaDbContext _dbContext;
    private readonly ILogger<ArticleRepository> _logger;
    private readonly BlogMapper _mapper;

    public ArticleRepository(
            BlogMapper mapper,
            TidjmaDbContext DbC,
            ILogger<ArticleRepository> logger
           )
    {
        _dbContext = DbC;
        _logger = logger;
        _mapper = mapper;
    }

    private OperationResult<T> OnSuccess<T>(T? payload = null)
        where T : class
    {
        var operationResult = new OperationResult<T>();
        operationResult.Payload = payload;
        operationResult.IsSuccess = true;

        return operationResult;
    }

    private OperationResult<T> OnFailure<T>(Exception? e = null, string errorMessage = null)
    {
        var operationResult = new OperationResult<T>();
        operationResult.ExceptionType = e;
        operationResult.ErrorMessage = errorMessage;
        operationResult.IsSuccess = false;

        return operationResult;
    }

    public OperationResult<ArticleDTO> Get(int id)
    {
        try 
        {
            return OnSuccess<ArticleDTO>(_mapper.ArticleToArticleDTO(_dbContext.Articles.Find(id)));
        } 
        catch (Exception e){

            return OnFailure<ArticleDTO>(e);
        }
    }

    public OperationResult<IEnumerable<ArticleDTO>> List() 
    { 
        try 
        {
            return OnSuccess<IEnumerable<ArticleDTO>>(
                 _dbContext.Articles
                .ToList()
                .Select(e => _mapper.ArticleToArticleDTO(e))
                );
        }
        catch (Exception e)
        {
            return OnFailure<IEnumerable<ArticleDTO>>(e);
        }
    }

    public OperationResult<object> Create(CreateArticleDTO newArticle)
    {
        try
        {
            _dbContext.Articles.Add(_mapper.CreateArticleDTOToArticle(newArticle)); 
            _dbContext.SaveChanges(); 

            return OnSuccess<object>();
        }
        catch (Exception e)
        {
            return OnFailure<object>(e);
        }

    }


    public OperationResult<object> Update(UpdateArticleDTO updatedArticle, int id)
    {
        try
        {
            _dbContext.Update(_mapper.UpdateArticleDTOToArticle(updatedArticle));
            _dbContext.SaveChanges();
            return OnSuccess<object>();
        }
        catch (Exception e)
        {
            return OnFailure<object>(e);
        }
    }

    public OperationResult<object> Delete(int id)
    {
        try
        {
            var article = _dbContext.Articles.Find(id);
            if (article is null)
            {
                return OnFailure<object>(null, "could not find ressource");
            }

            IEnumerable<Comment> comments = _dbContext.Comments
                .ToList()
                .Where(e => e.ArticleId == article.Id);
            _dbContext.Comments.RemoveRange(comments);
            _dbContext.Articles.Remove(article);

            _dbContext.SaveChanges();
            return OnSuccess<object>();
        }
        catch (Exception e)
        {
            return OnFailure<object>(e);
        }
    }
}
