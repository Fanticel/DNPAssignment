using Main;
using RepositoryContracts;

namespace InMemoryRepo;

public class PostInMemoryRepo : IPostRepo {
    private List<Post> postList;
    public PostInMemoryRepo() {
        postList = new List<Post>();
    }
    public Task<Post> AddAsync(Post post) {
        post.PostId = postList.Any() ? postList.Max(p => p.PostId) + 1 : 1;
        postList.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post) {
        Post? existingPost = postList.SingleOrDefault(p => p.PostId == post.PostId);
        if (existingPost is null) {
            throw new InvalidOperationException($"Post with ID {post.PostId} not found :(");
        }
        postList.Remove(existingPost);
        postList.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id) {
        Post? postToRemove = postList.SingleOrDefault(p => p.PostId == id);
        if (postToRemove is null) {
            throw new InvalidOperationException($"Post with ID {id} not found :(");
        }
        postList.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id) {
        Post? postToGet = postList.SingleOrDefault(p => p.PostId == id);
        if (postToGet is null) {
            throw new InvalidOperationException($"Post with id {id} not found :(");
        }
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMany() {
        return postList.AsQueryable();
    }
    public void CreateDummy() {
        AddAsync(new Post(1, "AMA: I spent a year living off-grid in the woods—ask me anything!", "After leaving my 9-to-5 job, I decided to live off-grid for a year. I built my own cabin, grew my own food, and learned a ton about self-sufficiency. If you’re curious about anything from my daily routine to the challenges I faced, feel free to ask!")).Result.PostComment(2);
        AddAsync(new Post(2, "AITA for not inviting my sister to my wedding after she tried to sabotage my engagement?", "So, I recently got engaged and was super excited to share the news. However, my sister made a huge scene at the family dinner, claiming I was 'stealing her thunder' since she had plans to get engaged soon. I decided not to invite her to my wedding. Now, my family is upset with me. AITA?"));
        AddAsync(new Post(3, "My partner wants to open our relationship, but I’m not sure. What should I do?", "My partner brought up the idea of opening our relationship, claiming it would make us stronger. I’m feeling unsure and a bit insecure about it. Has anyone gone through this? How did you handle it?")).Result.PostComment(3);
        AddAsync(new Post(4, "I accidentally ghosted my best friend for a month and now I’m scared to reach out.", "I got super busy with work and personal issues, and I didn't respond to texts or calls from my best friend. Now I feel terrible and don’t know how to apologize. What should I do?"));
        AddAsync(new Post(5, "What’s the most bizarre coincidence you’ve ever experienced?", "I once ran into my childhood best friend at a random airport halfway across the world. We hadn’t seen each other in 15 years! What are your craziest coincidence stories?")).Result.PostComment(1);
        postList[3].PostComment(4);
        postList[3].PostComment(5);
        Random r = new Random();
        foreach (Post p in postList) {
            p.Likes = r.Next(200);
            p.Dislikes = r.Next(200);
        }
    } 
}