namespace DTOs;

public class UpdatePostDto {
    public string PostTitle { get; set; }
    public string PostBody { get; set; }
    public int PosterId { get; set; }
    public List<int> Comments { get; set; }
}