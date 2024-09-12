namespace Tidjma.Helpers;
using Tidjma.Models;
using Tidjma.Contracts;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class BlogMapper
{
    // Comments
    public partial CommentDTO CommentToCommentDTO(Comment comment);

    public partial Comment UpdateCommentDTOToComment(UpdateCommentDTO updatedComment);

    public partial Comment CommentDTOToComment(CommentDTO commentDto);

    // Articles
    public partial Article CreateArticleDTOToArticle(CreateArticleDTO newArticle);

    public partial Article UpdateArticleDTOToArticle(UpdateArticleDTO updatedArticle);

    public partial ArticleDTO ArticleToArticleDTO(Article article);

    public partial Article ArticleDTOToArticle(ArticleDTO article);

    // Users
    public partial UserDTO UserToUserDTO(User user);

    public partial User UserDTOToUser(UserDTO userDTO);

    public partial User CreateUserDTOToUser(CreateUserDTO newUser);

    public partial User UpdateUserDTOToUser(UpdateUserDTO updatedUser);
}
