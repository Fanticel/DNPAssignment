using DTOs;
using Main;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace MeaningfulName.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController(ICommentRepo commentRepo, IUserRepo userRepo) : ControllerBase {
    private ICommentRepo _commentRepo = commentRepo;
    private IUserRepo _userRepo = userRepo;
    [HttpGet]
    public async Task<ActionResult<List<CommentDto>>> GetAllComments() {
        try {
            List<CommentDto> ans = new();
            foreach (Comment comment in _commentRepo.GetMany()) {
                ans.Add(await CreateDto(comment));
            }
            return Ok(ans);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("id={id:int}")]
    public async Task<ActionResult<CommentDto>> GetSingleComment([FromRoute] int id) {
        try {
            Comment c = await _commentRepo.GetSingleAsync(id);
            return Ok(await CreateDto(c));
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("posterid={id:int}")]
    public async Task<ActionResult<List<CommentDto>>> GetCommentByPoster([FromRoute] int posterid) {
        try {
            List<CommentDto> ans = new();
            foreach (Comment comment in _commentRepo.GetMany()) {
                if (comment.PosterId == posterid) {
                    ans.Add(await CreateDto(comment));
                }
            }
            return Ok(ans);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto commentDto) {
        try {
            Comment c = await _commentRepo.AddAsync(new Comment(commentDto.PosterId, commentDto.CommentBody));
            return Ok(await CreateDto(c));
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("/{id:int}")]
    public async Task<ActionResult> RemoveComment([FromRoute] int id) {
        try {
            await _commentRepo.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("/{id:int}")]
    public async Task<ActionResult> UpdateComment([FromRoute] int id, [FromBody] CreateCommentDto commentDto) {
        try {
            Comment c = new(commentDto.PosterId, commentDto.CommentBody) {
                Id = id
            };
            await _commentRepo.UpdateAsync(c);
            return Ok(await CreateDto(c));
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    private async Task<CommentDto> CreateDto(Comment comment) {
        User creator = await _userRepo.GetSingleAsync(comment.PosterId);
        return (new CommentDto {
            Id = comment.Id,
            CommentBody = comment.CommentBody,
            PosterName = creator.UserName
        });
    }
}