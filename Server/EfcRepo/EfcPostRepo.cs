using Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepo;

public class EfcPostRepo : IPostRepo {
    private readonly AppContext _ctx;
    public EfcPostRepo(AppContext ctx) {
        _ctx = ctx;
    }
    public async Task<Post> AddAsync(Post post) {
        EntityEntry<Post> entityEntry = await _ctx.postSet.AddAsync(post);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task UpdateAsync(Post post) {
        if (!(await _ctx.postSet.AnyAsync(p => p.PostId == post.PostId))) {
            throw new InvalidOperationException($"Post with {post.PostId} not found");
        }
        _ctx.postSet.Update(post);
        await _ctx.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        Post? attempted = await _ctx.postSet.SingleOrDefaultAsync(p => p.PostId == id);
        if (attempted is null) {
            throw new InvalidOperationException($"Post with {id} not found");
        }
        _ctx.postSet.Remove(attempted);
        await _ctx.SaveChangesAsync();
    }
    public async Task<Post> GetSingleAsync(int id) {
        Post? attempted = await _ctx.postSet.SingleOrDefaultAsync(p => p.PostId == id);
        if (attempted is null) {
            throw new InvalidOperationException($"Post with {id} not found");
        }
        return attempted;
    }
    public IQueryable<Post> GetMany() {
        return _ctx.postSet.AsQueryable();
    }
}