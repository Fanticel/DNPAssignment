using System.Text.Json;
using DTOs;

namespace Client.Services;

public class HttpUserService: IUserService {
    private readonly HttpClient _client;
    public HttpUserService(HttpClient client) {
        _client = client;
    }
    public async Task<UserDto> AddUserAsync(CreateUserDto request) {
        HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("Users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        })!;
    }
}