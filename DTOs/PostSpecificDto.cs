using Main;

namespace DTOs;

public class PostSpecificDto {
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public string PosterName { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public List<CommentDto> Comments { get; set; }
    public PostSpecificDto(Post post) {
        PostTitle = post.PostTitle;
        PostBody = post.PostBody;
        Likes = post.Likes;
        Dislikes = post.Dislikes;
    }
}