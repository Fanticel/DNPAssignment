using Main;

namespace DTOs;

public class PostGeneralDto {
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public string PosterName { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public PostGeneralDto(Post post) {
        PostTitle = post.PostTitle;
        PostBody = post.PostBody;
        Likes = post.Likes;
        Dislikes = post.Dislikes;
    }
}