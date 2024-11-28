using Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepo;

public class EfcCommentRepo : ICommentRepo {
    private readonly AppContext _ctx;
    public EfcCommentRepo(AppContext ctx) {
        _ctx = ctx;
    }
    public async Task<Comment> AddAsync(Comment comment) {
        EntityEntry<Comment> entityEntry = await _ctx.commentSet.AddAsync(comment);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task UpdateAsync(Comment comment) {
        if (!(await _ctx.commentSet.AnyAsync(c => c.Id == comment.Id))) {
            throw new InvalidOperationException($"Comment with {comment.Id} not found");
        }
        _ctx.commentSet.Update(comment);
        await _ctx.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        Comment? attempted = await _ctx.commentSet.SingleOrDefaultAsync(c => c.Id == id);
        if (attempted is null) {
            throw new InvalidOperationException($"Comment with {id} not found");
        }
        _ctx.commentSet.Remove(attempted);
        await _ctx.SaveChangesAsync();
    }
    public async Task<Comment> GetSingleAsync(int id) {
        Comment? attempted = await _ctx.commentSet.SingleOrDefaultAsync(c => c.Id == id);
        if (attempted is null) {
            throw new InvalidOperationException($"Comment with {id} not found");
        }
        return attempted;
    }
    public IQueryable<Comment> GetMany() {
        return _ctx.commentSet.AsQueryable();
    }
}