using api.Dtos.Comment;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync (CommentQueryObject query); // Get all Comments
    Task<List<Comment>> GetCommentsByProductIdAsync (CommentQueryObject query, int productId); // Get all Comments of one Product
    Task<Comment?> GetByIdAsync (int commentId); // Get Comment by Id 
    Task<Comment?> GetByIdAndUserIdAsync (int commentId, string userId, bool isAdmin); // Get Comment by Id and by User Id
    Task<Comment> CreateAsync (Comment commentModel); // Create Comment
    Task<Comment?> Delete (int commentId); // Delete Comment
    Task<Comment?> Patch (int commentId, PatchCommentDto patchDto); // Patch Comment
}