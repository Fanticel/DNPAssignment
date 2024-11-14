using System.Text.Json;
using DTOs;

namespace Client.Services;

public class HttpPostService: IPostService {
    private readonly HttpClient _client;
    public HttpPostService(HttpClient client) {
        _client = client;
    }
    public async Task<PostGeneralDto> CreatePostAsync(CreatePostDto createPostDto) {
        HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("Posts", createPostDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostGeneralDto>(response, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
    }
    public async Task<List<PostGeneralDto>> GetAllPosts() {
        HttpResponseMessage httpResponse = await _client.GetAsync("Posts");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<List<PostGeneralDto>>(response, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
    }
    public async Task<PostSpecificDto> GetSinglePost(int id) {
        HttpResponseMessage httpResponse = await _client.GetAsync("Posts/"+id);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostSpecificDto>(response, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
    }
    public async Task UpdatePost(int id, UpdatePostDto postDto) {
        HttpResponseMessage httpResponse = await _client.PatchAsJsonAsync("Posts/"+id, postDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        }
    }
}