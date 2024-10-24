namespace Main;

public class Post {
    //TODO: probably switch the likes/dislikes from a counter to a list of users that liked?
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public int PostId { get; set; }
    public int PosterId { get; set; }
    public List<int> CommentIds { get; set; }
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    
    public Post(int posterId, string postTitle, string postBody) {
        Likes = 0;
        Dislikes = 0;
        PosterId = posterId;
        PostTitle = postTitle;
        PostBody = postBody;
        CommentIds = new List<int>();
    }

    public void PostComment(int commentId) {
        CommentIds.Add(commentId);
    }
    public void Like() {
        Likes++;
    }
    public void Dislike() {
        Dislikes++;
    }
    public override string ToString() {
        return $"{PostTitle}, {PostBody}, {PosterId}";
    }
}