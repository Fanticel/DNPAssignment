using DTOs;
using Main;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace MeaningfulName.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase {
    private IPostRepo _postRepo;
    public PostsController(IPostRepo postRepo) {
        _postRepo = postRepo;
    }
    [HttpGet]
    public async Task<ActionResult<List<PostGeneralDto>>> GetAllPosts() {
        List<PostGeneralDto> ans = new();
        foreach (Post post in _postRepo.GetMany()) {
            Console.WriteLine(post);
            ans.Add(new PostGeneralDto(post));
        }
        Console.WriteLine(ans);
        return Ok(ans);
    }
}