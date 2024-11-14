using Main;

namespace DTOs;

public class PostSpecificDto {
    public int PostId { get; set; }
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public int PosterId { get; set; }
    public string PosterName { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public List<CommentDto> Comments { get; set; }
}