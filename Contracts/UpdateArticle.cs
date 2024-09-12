namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;

/*
    Used for updating existing Articles inside an article context
    then UpdateArticleDTO will be mapped into an Article Model
*/

public record UpdateArticleDTO(
        int Id,
        [Required][StringLength(100)] string Title,
        [Required] int AuthorId,
        [Required][StringLength(1000)] string Body,
        [Required][MinLength(1)] string[] Tags,
        [Required] DateOnly Release,
        [Required] int UpVote,
        int DownVote);
