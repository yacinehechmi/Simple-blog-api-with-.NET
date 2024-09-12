namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;

/*
    Used for inserting new Comments inside a comment context
    within a specific article such as "/articles/{article_id}/comments/"

    then CreateCommentDTO will be mapped into a Comment Model
*/

public record CreateCommentDTO(
        [Required] int AuthorId,
        [Required] int ArticleId,
        [Required][StringLength(300)] string Body);
