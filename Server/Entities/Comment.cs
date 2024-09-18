namespace Main;

public class Comment {
    private int posterId;
    public string CommentBody { get; set; }
    public int Id { get; set; }
    public Comment(int posterId, string commentBody) {
        this.posterId = posterId;
        CommentBody = commentBody;
    }
    public int getPoster() {
        return posterId;
    }
}