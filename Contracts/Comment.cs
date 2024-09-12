namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;

/*
    Used for quering for Comments inside an article such as the 
    the endpoint would look something like "/articles/{article_id}/comments/"
    from a Database
    then Comment Models are going to be mapped into
    List<CommentDTO>
    
    Or when quering for one Comment by id it will return 
    an CommentDTO to a client
*/

public record CommentDTO(
        [Required] int Id,
        [Required] int ArticleId,
        [Required] int AuthorId,
        [Required] string Body);

