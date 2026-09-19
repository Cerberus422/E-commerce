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

    [HttpGet]
    [Authorize(Roles = "Admin")] 
    
    public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject query) // Showing all Comments, with filters 
    {
        var comments = await _commentRepo.GetAllAsync(query);

        var commentsModel = comments.Select(c => c.ToCommentDto()).ToList();

        return Ok(commentsModel);
    }
    
    [HttpGet("{productId:int}")]
    [Authorize] 
    public async Task<IActionResult> GetCommentsForProduct([FromQuery] CommentQueryObject query, int productId) // Showing all Comments of one Product 
    {
        var product = await _productRepo.GetByIdAsync(productId);

        if (product == null)
            return NotFound("Product not found");
        
        var comments = await _commentRepo.GetCommentsByProductId(query, productId);

        var commentsModel = comments.Select(c => c.ToCommentDto()).ToList();

        return Ok(commentsModel);
    }
}