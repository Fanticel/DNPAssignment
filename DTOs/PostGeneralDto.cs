using Main;

namespace DTOs;

public class PostGeneralDto {
    public int PostId { get; set; }
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public string PosterName { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}