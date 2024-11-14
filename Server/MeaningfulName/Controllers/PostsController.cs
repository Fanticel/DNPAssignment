using DTOs;
using Main;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace MeaningfulName.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase {
    private IPostRepo _postRepo;
    private IUserRepo _userRepo;
    private ICommentRepo _commentRepo;
    public PostsController(IPostRepo postRepo, IUserRepo userRepo, ICommentRepo commentRepo) {
        _postRepo = postRepo;
        _userRepo = userRepo;
        _commentRepo = commentRepo;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<PostGeneralDto>>> GetAllPosts() {
        List<PostGeneralDto> ans = new();
        foreach (Post post in _postRepo.GetMany()) {
            User creator = await _userRepo.GetSingleAsync(post.PosterId);
            ans.Add(new PostGeneralDto{
                PostId = post.PostId,
                PostTitle = post.PostTitle,
                PostBody = post.PostBody,
                PosterName = creator.UserName,
                Likes = post.Likes,
                Dislikes = post.Dislikes
            });
        }
        return Ok(ans);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostSpecificDto>> GetSinglePost([FromRoute] int id) {
        try {
            Post post = await _postRepo.GetSingleAsync(id);
            User creator = await _userRepo.GetSingleAsync(post.PosterId);
            List<CommentDto> commentDtos = new();
            foreach (int comId in post.CommentIds) {
                Comment c = await _commentRepo.GetSingleAsync(comId);
                User ccreator = await _userRepo.GetSingleAsync(c.PosterId);
                commentDtos.Add(new CommentDto {
                    Id = c.Id,
                    CommentBody = c.CommentBody,
                    PosterName = ccreator.UserName
                });
            }
            PostSpecificDto ans = new() {
                PostId = post.PostId,
                PostTitle = post.PostTitle,
                PostBody = post.PostBody,
                PosterName = creator.UserName,
                PosterId = creator.Id,
                Likes = post.Likes,
                Dislikes = post.Dislikes,
                Comments = commentDtos
            };
            return Ok(ans);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("/has={subString}")]
    public async Task<ActionResult<List<PostGeneralDto>>> GetPostsByTitle([FromRoute] String subString) {
        try {
            List<PostGeneralDto> ans = new();
            foreach (Post post in _postRepo.GetMany()) {
                User creator = await _userRepo.GetSingleAsync(post.PosterId);
                if (post.PostTitle.Contains(subString)) {
                    ans.Add(new PostGeneralDto{
                        PostId = post.PostId,
                        PostTitle = post.PostTitle,
                        PostBody = post.PostBody,
                        PosterName = creator.UserName,
                        Likes = post.Likes,
                        Dislikes = post.Dislikes
                    });
                }
            }
            return Ok(ans);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<PostGeneralDto>> AddPost([FromBody] CreatePostDto createPostDto) {
        try {
            Post post = await _postRepo.AddAsync(new Post(createPostDto.PosterId, createPostDto.PostTitle, createPostDto.PostBody));
            User creator = await _userRepo.GetSingleAsync(post.PosterId);
            PostGeneralDto answer = new() {
                PostId = post.PostId,
                PostTitle = post.PostTitle,
                PostBody = post.PostBody,
                PosterName = creator.UserName,
                Likes = post.Likes,
                Dislikes = post.Dislikes
            };
            return Ok(answer);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemovePost([FromRoute] int id) {
        try {
            await _postRepo.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdatePost([FromRoute] int id,[FromBody] UpdatePostDto postDto) {
        Post post = new Post(postDto.PosterId, postDto.PostTitle, postDto.PostBody);
        post.CommentIds = postDto.Comments;
        post.PostId = id;
        try {
            await _postRepo.UpdateAsync(post);
            return Ok();
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}