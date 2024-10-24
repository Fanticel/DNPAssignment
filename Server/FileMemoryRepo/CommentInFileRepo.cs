using System.Text.Json;
using Main;
using RepositoryContracts;

namespace FileMemoryRepo;

public class CommentInFileRepo : ICommentRepo {
    private readonly string _filePath = "CommentFile.json";

    public CommentInFileRepo() {
        if (!File.Exists(_filePath)) {
            File.WriteAllText(_filePath, "[]");
            CreateDummy();
        }
    }

    public async Task<Comment> AddAsync(Comment comment) {
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(await File.ReadAllTextAsync(_filePath))!;
        comment.Id = comments.Any() ? comments.Max(d => d.Id) + 1 : 1;
        comments.Add(comment);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(comments));
        return comment;
    }
    public async Task UpdateAsync(Comment comment) {
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(await File.ReadAllTextAsync(_filePath))!;
        Comment? existingComment = comments.SingleOrDefault(d => d.Id == comment.Id);
        if (existingComment is null) {
            throw new InvalidOperationException($"Comment with id {comment.Id} not found :(");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(comments));
    }
    public async Task DeleteAsync(int id) {
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(await File.ReadAllTextAsync(_filePath))!;
        Comment? existingComment = comments.SingleOrDefault(d => d.Id == id);
        if (existingComment is null) {
            throw new InvalidOperationException($"Comment with id {id} not found :(");
        }
        comments.Remove(existingComment);
        await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(comments));
    }
    public async Task<Comment> GetSingleAsync(int id) {
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(await File.ReadAllTextAsync(_filePath))!;
        Comment? existingComment = comments.SingleOrDefault(d => d.Id == id);
        if (existingComment is null) {
            throw new InvalidOperationException($"Comment with id {id} not found :(");
        }
        return existingComment;
    }
    public IQueryable<Comment> GetMany() {
        return JsonSerializer.Deserialize<List<Comment>>(File.ReadAllTextAsync(_filePath).Result)!.AsQueryable();
    }
    public async void CreateDummy() {
        await AddAsync(new Comment(1, "Very cool"));
        await AddAsync(new Comment(2, "You suck"));
        await AddAsync(new Comment(1, "Pog"));
        await AddAsync(new Comment(3, "AY dios mio"));
        await AddAsync(new Comment(3, "Very cool"));
    }
}