namespace Main;

public class Comment {
    private User CommentOp;
    public string CommentBody { get; set; }
    public Comment(User Op, string commentBody) {
        CommentOp = Op;
        CommentBody = commentBody;
    }

    public override string ToString() {
        return $"\t\t\t\t{CommentOp.UserName}\n\t\t\t{CommentBody}";
    }
}