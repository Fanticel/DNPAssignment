using System.Text.Json;
using DTOs;

namespace Client.Services;

public class HttpCommentService: ICommentService {
    private readonly HttpClient _client;
    public HttpCommentService(HttpClient client) {
        _client = client;
    }
    public async Task<CommentDto> CreateComment(CreateCommentDto commentDto) {
        HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("Comment", commentDto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
    }
}