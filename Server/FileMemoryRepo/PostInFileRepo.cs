using System.Text.Json;
using Main;
using RepositoryContracts;

namespace FileMemoryRepo;

public class PostInFileRepo : IPostRepo {
    private readonly string _filePath = "PostFile.json";
    public PostInFileRepo() {
        if (!File.Exists(_filePath)) {
            Console.WriteLine("daw");
            File.WriteAllText(_filePath, "[]");
        }
    }
    public async Task<Post> AddAsync(Post post) {
        List<Post> listPost = JsonSerializer.Deserialize<List<Post>>(await File.ReadAllTextAsync(_filePath))!;
        post.PostId = listPost.Any() ? listPost.Max(d => d.PostId) + 1 : 1;
        listPost.Add(post);
        await File.WriteAllTextAsync(_filePath,JsonSerializer.Serialize(listPost));
        return post;
    }
    public async Task UpdateAsync(Post post) {
        List<Post> listPost = JsonSerializer.Deserialize<List<Post>>(await File.ReadAllTextAsync(_filePath))!;
        Post? postInQuestion = listPost.SingleOrDefault(d => d.PostId == post.PostId);
        if (postInQuestion is null) {
            throw new InvalidOperationException($"Post with ID {post.PostId} not found :(");
        }
        listPost.Remove(postInQuestion);
        listPost.Add(post);
        await File.WriteAllTextAsync(_filePath,JsonSerializer.Serialize(listPost));
    }
    public async Task DeleteAsync(int id) {
        List<Post> listPost = JsonSerializer.Deserialize<List<Post>>(await File.ReadAllTextAsync(_filePath))!;
        Post? postInQuestion = listPost.SingleOrDefault(d => d.PostId == id);
        if (postInQuestion is null) {
            throw new InvalidOperationException($"Post with ID {id} not found :(");
        }
        listPost.Remove(postInQuestion);
        await File.WriteAllTextAsync(_filePath,JsonSerializer.Serialize(listPost));
    }
    public async Task<Post> GetSingleAsync(int id) {
        List<Post> listPost = JsonSerializer.Deserialize<List<Post>>(await File.ReadAllTextAsync(_filePath))!;
        Post? postInQuestion = listPost.SingleOrDefault(d => d.PostId == id);
        if (postInQuestion is null) {
            throw new InvalidOperationException($"Post with ID {id} not found :(");
        }
        return postInQuestion;
    }
    public IQueryable<Post> GetMany() {
        return JsonSerializer.Deserialize<List<Post>>(File.ReadAllTextAsync(_filePath).Result)!.AsQueryable();
    }
    public async void CreateDummy() {
        GetSingleAsync(2).Result.PostComment(4);
        GetSingleAsync(3).Result.PostComment(4);
        GetSingleAsync(3).Result.PostComment(5);
        GetSingleAsync(4).Result.PostComment(5);
        GetSingleAsync(3).Result.PostComment(5);
        Random r = new ();
        foreach (Post p in GetMany()) {
            p.Likes = r.Next(200);
            p.Dislikes = r.Next(200);
        }
    }
}