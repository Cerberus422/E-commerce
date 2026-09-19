using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            Rating = commentModel.Rating,
            CreatedOn = commentModel.CreatedOn,
            ProductId = commentModel.ProductId,
            ProductName = commentModel.Product.Name,
            Username = commentModel.AppUser.UserName
        };
    }
}