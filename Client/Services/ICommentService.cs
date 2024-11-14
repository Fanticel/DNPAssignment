using DTOs;

namespace Client.Services;

public interface ICommentService {
    Task<CommentDto> CreateComment(CreateCommentDto commentDto);
}