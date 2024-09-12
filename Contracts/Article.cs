namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;

/*
    Used for quering Articles from a Database
    then Article Models are going to be mapped into
    List<ArticleDTO>
    
    Or when quering for one Article by id it will return 
    an ArticleDTO to a client
*/
public record ArticleDTO(
        [Required] int Id,
        [Required][StringLength(100)] string Title,
        [Required] int AuthorId,
        [Required][StringLength(1000)] string Body,
        int UpVote,
        int DownVote);
