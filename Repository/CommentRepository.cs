using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync(CommentQueryObject query)
    {
        var comments = _context.Comments
            .Include(c => c.Product)
            .Include(c => c.AppUser)
            .AsQueryable();

        if (query.Rating.HasValue)
        {
            comments = comments
                .Where(c => c.Rating == query.Rating);
        }

        if (query.MinRating.HasValue)
        {
            comments = comments
                .Where(c => c.Rating >= query.MinRating);
        }
        
        if (query.MaxRating.HasValue)
        {
            comments = comments
                .Where(c => c.Rating <= query.MaxRating);
        }

        if (query.CreatedAfter.HasValue)
        {
            comments = comments
                .Where(c => c.CreatedOn >= query.CreatedAfter);
        }
        
        if(query.CreatedBefore.HasValue)
        {
            comments = comments
                .Where(c => c.CreatedOn <= query.CreatedBefore);
        }

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            if (query.SortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase))
            {
                comments = query.IsDescending
                    ? comments.OrderByDescending(c => c.Rating)
                    : comments.OrderBy(c => c.Rating);
            }
            else if (query.SortBy.Equals("CreatedOn", StringComparison.OrdinalIgnoreCase))
            {
                comments = query.IsDescending
                    ? comments.OrderByDescending(c => c.CreatedOn)
                    : comments.OrderBy(c => c.CreatedOn);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await comments.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsByProductIdAsync(CommentQueryObject query, int productId)
    {
        var comments = _context.Comments
            .Include(c => c.Product)
            .Include(c => c.AppUser)
            .Where(c => c.ProductId == productId)
            .AsQueryable();

        var product = await _context.Products.FindAsync(productId);
        
        
        if (query.Rating.HasValue)
        {
            comments = comments
                .Where(c => c.Rating == query.Rating);
        }

        if (query.MinRating.HasValue)
        {
            comments = comments
                .Where(c => c.Rating >= query.MinRating);
        }
        
        if (query.MaxRating.HasValue)
        {
            comments = comments
                .Where(c => c.Rating <= query.MaxRating);
        }

        if (query.CreatedAfter.HasValue)
        {
            comments = comments
                .Where(c => c.CreatedOn >= query.CreatedAfter);
        }
        
        if(query.CreatedBefore.HasValue)
        {
            comments = comments
                .Where(c => c.CreatedOn <= query.CreatedBefore);
        }

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            if (query.SortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase))
            {
                comments = query.IsDescending
                    ? comments.OrderByDescending(c => c.Rating)
                    : comments.OrderBy(c => c.Rating);
            }
            else if (query.SortBy.Equals("CreatedOn", StringComparison.OrdinalIgnoreCase))
            {
                comments = query.IsDescending
                    ? comments.OrderByDescending(c => c.CreatedOn)
                    : comments.OrderBy(c => c.CreatedOn);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await comments.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int commentId)
    {
        var comment = await _context.Comments
            .Include(c => c.Product)
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(c => c.id == commentId);

        return comment;
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        
        return commentModel;
    }
}