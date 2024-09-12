namespace Tidjma.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("article")]
public class Article 
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public required string Title { get; set; }

    [Column("body")]
    public required string Body { get; set; }

    [Column("author_id")]
    public required int AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public User? User { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    [Column("up_votes")]
    public int UpVote { get; set; }

    [Column("down_votes")]
    public int DownVote { get; set; }
}
