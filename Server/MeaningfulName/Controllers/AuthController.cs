using DTOs;
using Main;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace MeaningfulName.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserRepo userRepo): ControllerBase {
    private IUserRepo _userRepo = userRepo;
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> PostLoginAttempt([FromBody]LoginRequestDto request) {
        User? attemptedUser = _userRepo.GetMany().SingleOrDefault(d => d.UserName == request.Username);
        if (attemptedUser is null || attemptedUser.UserPass != request.Password) {
            return Unauthorized("Incorrect data");
        }
        return new UserDto{
            UserName = attemptedUser.UserName,
            Id = attemptedUser.Id
        };
    }
}