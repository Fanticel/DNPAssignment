using Main;

namespace DTOs;

public class CommentDto {
    public String PosterName { get; set; }
    public string CommentBody { get; set; }
    public CommentDto(Comment comment) {
        CommentBody = comment.CommentBody;
    }
}