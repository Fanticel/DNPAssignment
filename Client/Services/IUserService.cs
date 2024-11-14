using DTOs;

namespace Client.Services;

public interface IUserService {
    public Task<UserDto> AddUserAsync(CreateUserDto request);
}