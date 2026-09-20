using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IProductRepository _productRepo;

    public CommentController(ICommentRepository commentRepo, IProductRepository productRepo)
    {
        _commentRepo = commentRepo;
        _productRepo = productRepo;
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")] 
    
    public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query) // Showing all Comments, with filters 
    {
        var comments = await _commentRepo.GetAllAsync(query);

        var commentsModel = comments.Select(c => c.ToCommentDto()).ToList();

        return Ok(commentsModel);
    }
    
    [HttpGet("product/{productId:int}")]
    [Authorize] 
    public async Task<IActionResult> GetCommentsForProduct([FromQuery] CommentQueryObject query, [FromRoute] int productId) // Showing all Comments of one Product 
    {
        var product = await _productRepo.GetByIdAsync(productId);

        if (product == null)
            return NotFound("Product not found");
        
        var comments = await _commentRepo.GetCommentsByProductIdAsync(query, productId);

        var commentsModel = comments.Select(c => c.ToCommentDto()).ToList();

        return Ok(commentsModel);
    }
    
    [HttpGet("{commentId:int}")]
    [Authorize] 
    public async Task<IActionResult> GetById([FromRoute] int  commentId) // Get Comment by Id 
    {
        var comment = await _commentRepo.GetByIdAsync(commentId);

        if (comment == null)
            return NotFound("Comment not found");

        var commentModel = comment.ToCommentDto();

        return Ok(commentModel);
    }
    
    [HttpPost("product/{productId:int}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] CreateCommentDto commentDto) // Create comment by ProductId
    {
        if (!await _productRepo.ProductExists(productId))
        {
            return NotFound("Product does not exist");
        }

        var userId = User.GetUserId();
        
        var commentModel = commentDto.ToCommentFromCreate(productId);

        commentModel.AppUserId = userId;

        await _commentRepo.CreateAsync(commentModel);

        var createdComment = await _commentRepo.GetByIdAsync(commentModel.id);
        
        return CreatedAtAction(
            nameof(GetById),
            new { commentId = createdComment!.id },
            createdComment.ToCommentDto());
    }
    
}