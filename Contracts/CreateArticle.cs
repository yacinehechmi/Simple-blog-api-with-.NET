namespace Tidjma.Contracts;

/*
    Used for inserting new Articles inside an article context
    then CreateArticleDTO will be mapped into an Article Model

    UpVotes and DownVotes are going to be zeros
*/

public class CreateArticleDTO
{
        public string? Title { get; set; }
        public int AuthorId { get; set; }
        public DateOnly Release { get; set; }
        public string? Body { get; set; }
}
