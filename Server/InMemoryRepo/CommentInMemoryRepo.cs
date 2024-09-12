using Main;
using RepositoryContracts;
using InvalidOperationException = System.InvalidOperationException;

namespace InMemoryRepo;

public class CommentInMemoryRepo : ICommentRepo {
    private List<Comment> commentList;

    public Task<Comment> AddAsync(Comment comment) {
        comment.Id = commentList.Any() ? commentList.Max(c => c.Id) + 1 : 1;
        commentList.Add(comment);
        return Task.FromResult(comment);
    }
    public Task UpdateAsync(Comment comment) {
        Comment? existingComment = commentList.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null) {
            throw new InvalidOperationException($"Comment with ID {comment.Id} not found :(");
        }
        commentList.Remove(existingComment);
        commentList.Add(comment);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(int id) {
        Comment? commentToRemove = commentList.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null) {
            throw new InvalidOperationException($"Comment with id {id} not found :(");
        }
        commentList.Remove(commentToRemove);
        return Task.CompletedTask;
    }
    public Task<Comment> GetSingleAsync(int id) {
        Comment? commentToGet = commentList.SingleOrDefault(c => c.Id == id);
        if (commentToGet is null) {
            throw new InvalidOperationException($"Comment with id {id} not found :(");
        }
        return Task.FromResult(commentToGet);
    }
    public IQueryable<Comment> GetMany() {
        return commentList.AsQueryable();
    }
}