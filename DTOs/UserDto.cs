using Main;

namespace DTOs;

public class UserDto {
    public String UserName { get; set; }
    public int Id { get; set; }
    public UserDto(User user) {
        UserName = user.UserName;
        Id = user.Id;
    }
}