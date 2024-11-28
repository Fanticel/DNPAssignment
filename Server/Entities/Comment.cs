namespace Main;

public class Comment {
    public int Id { get; set; }
    public int PosterId { get; set; }
    public string CommentBody { get; set; }
    private Comment(){} //for EFC
    public Comment(int posterId, string commentBody) {
        PosterId = posterId;
        CommentBody = commentBody;
    }
}