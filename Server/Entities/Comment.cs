namespace Main;

public class Comment {
    public int PosterId { get; set; }
    public string CommentBody { get; set; }
    public int Id { get; set; }
    public Comment(int posterId, string commentBody) {
        PosterId = posterId;
        CommentBody = commentBody;
    }
}