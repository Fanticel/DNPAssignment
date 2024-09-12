using Main;
using RepositoryContracts;

namespace InMemoryRepo;

public class PostInMemoryRepo : IPostRepo {
    private List<Post> postList;

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
}