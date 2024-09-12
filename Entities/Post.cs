namespace Main;

public class Post {
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public int PostId { get; set; }
    public User Op { get; set; }
    public List<Comment> Comments { get; set; }
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    
    public Post(User Op, string postTitle, string postBody) {
        Likes = 0;
        Dislikes = 0;
        this.Op = Op;
        PostTitle = postTitle;
        PostBody = postBody;
        Comments = new List<Comment>();
    }

    public void PostComment(User CommentOp, string content) {
        Comments.Add(new Comment(CommentOp, content));
    }
    
    public override string ToString() {
        return $"OP:{Op.UserName}\n___{PostTitle}___\n\t{Likes} | {Dislikes}";
    }
    public string FullToString() {
        string ans = $"OP:{Op.UserName}\n___{PostTitle}___\n{PostBody}\n\t{Likes} | {Dislikes}\n";
        foreach (Comment com in Comments) {
            ans += com + "\n";
        }
        return ans;
    }
    public void Like() {
        Likes++;
    }
    public void Dislike() {
        Dislikes++;
    }
}