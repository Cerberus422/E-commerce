using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(CommentQueryObject query); // Get all Comments
    Task<List<Comment>> GetCommentsByProductId(CommentQueryObject query, int productId); // Get all Comments of one Product
}