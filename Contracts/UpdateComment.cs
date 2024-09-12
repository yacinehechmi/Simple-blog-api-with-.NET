namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;

/*
    Used for updating existing Comments inside a comment context
    within a specific article such as "/articles/{article_id}/comments/{comment_id}"
    then UpdateCommentDTO will be mapped into an Comment Model
*/

public record UpdateCommentDTO(
        int Id,
        [Required] int AuthorId,
        [Required] int ArticleId,
        [Required][StringLength(300)] string Body);
