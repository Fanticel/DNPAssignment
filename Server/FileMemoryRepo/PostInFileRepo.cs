using System.Text.Json;
using Main;
using RepositoryContracts;

namespace FileMemoryRepo;

public class PostInFileRepo : IPostRepo {
    private readonly string _filePath = "PostFile.json";
    public PostInFileRepo() {
        if (!File.Exists(_filePath)) {
            File.WriteAllText(_filePath, "[]");
            CreateDummy();
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
        await AddAsync(new Post(1, "AMA: I spent a year living off-grid in the woods—ask me anything!", "After leaving my 9-to-5 job, I decided to live off-grid for a year. I built my own cabin, grew my own food, and learned a ton about self-sufficiency. If you’re curious about anything from my daily routine to the challenges I faced, feel free to ask!"));
        await AddAsync(new Post(2, "AITA for not inviting my sister to my wedding after she tried to sabotage my engagement?", "So, I recently got engaged and was super excited to share the news. However, my sister made a huge scene at the family dinner, claiming I was 'stealing her thunder' since she had plans to get engaged soon. I decided not to invite her to my wedding. Now, my family is upset with me. AITA?"));
        await AddAsync(new Post(3, "My partner wants to open our relationship, but I’m not sure. What should I do?", "My partner brought up the idea of opening our relationship, claiming it would make us stronger. I’m feeling unsure and a bit insecure about it. Has anyone gone through this? How did you handle it?"));
        await AddAsync(new Post(4, "I accidentally ghosted my best friend for a month and now I’m scared to reach out.", "I got super busy with work and personal issues, and I didn't respond to texts or calls from my best friend. Now I feel terrible and don’t know how to apologize. What should I do?"));
        await AddAsync(new Post(5, "What’s the most bizarre coincidence you’ve ever experienced?", "I once ran into my childhood best friend at a random airport halfway across the world. We hadn’t seen each other in 15 years! What are your craziest coincidence stories?"));
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