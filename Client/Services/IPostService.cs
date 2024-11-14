using DTOs;

namespace Client.Services;

public interface IPostService {
    public Task<PostGeneralDto> CreatePostAsync(CreatePostDto createPostDto);
    public Task<List<PostGeneralDto>> GetAllPosts();
    public Task<PostSpecificDto> GetSinglePost(int id);
    public Task UpdatePost(int id, UpdatePostDto postDto);
}