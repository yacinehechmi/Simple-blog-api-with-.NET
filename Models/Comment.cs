namespace Tidjma.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("comment")]
public class Comment 
{
    [Column("id")]
    public int Id { get; set; }

    [Column("body")]
    public required string Body { get; set; }

    [Column("article_id")]
    public int? ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article? Article { get; set; }

    [Column("author_id")]
    public int AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public User? User { get; set; }
}

