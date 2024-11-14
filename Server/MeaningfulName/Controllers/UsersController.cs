using DTOs;
using Main;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace MeaningfulName.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase {
    private IUserRepo _userRepo;
    public UsersController(IUserRepo userRepo) {
        _userRepo = userRepo;
    }
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto req) {
        if (UserValid(req.UserName)) {
            User created = await _userRepo.AddAsync(new User(req.UserName, req.Password));
            UserDto userDto = new() {
                Id = created.Id,
                UserName = created.UserName
            };
            return Created($"/Users/{userDto.Id}", userDto);
        }

        return BadRequest("Username taken");
    }
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers() {
        List<UserDto> ans = new();
        foreach (User user in _userRepo.GetMany()) {
            ans.Add(new UserDto(){
                Id = user.Id,
                UserName = user.UserName
            });
        }
        return Ok(ans);
    }
    [HttpGet("ByName/{nameSubString}")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsersWithString([FromRoute] string nameSubString) {
        List<UserDto> ans = new();
        foreach (User user in _userRepo.GetMany()) {
            if (user.UserName.Contains(nameSubString))
            {
                ans.Add(new UserDto(){
                    Id = user.Id,
                    UserName = user.UserName
                });
            }
        }
        return Ok(ans);
    }
    [HttpGet("ById/{id:int}")]
    public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id) {
        try {
            User user = await _userRepo.GetSingleAsync(id);
            Console.WriteLine(user);
            return Ok(new UserDto(){
                Id = user.Id,
                UserName = user.UserName
            });
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] CreateUserDto userDto) {
        try {
            User user = await _userRepo.GetSingleAsync(id);
            if (user.UserPass == userDto.Password) {
                user.UserName = userDto.UserName;
                await _userRepo.UpdateAsync(user);
                return Ok();
            }
            return BadRequest("Incorrect password");
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id, [FromBody] string password) {
        try {
            User user = await _userRepo.GetSingleAsync(id);
            if (user.UserPass == password) {
                await _userRepo.DeleteAsync(user.Id);
                return Ok();
            }
            return BadRequest("Incorrect password");
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
        
    private bool UserValid(string username) {
        foreach (User user in _userRepo.GetMany()) {
            if (user.UserName == username) {
                return false;
            }
        }
        return true;
    }
}