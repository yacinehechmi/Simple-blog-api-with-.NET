namespace Tidjma.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User 
{
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    public required string Username { get; set; }

    [Column("password")]
    public required string Password { get; set; }
    
    [Column("role")]
    public required string Role { get; set; }

    public ICollection<Article>? Articles { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    [Column("email")]
    public required string Email { get; set; }
}
