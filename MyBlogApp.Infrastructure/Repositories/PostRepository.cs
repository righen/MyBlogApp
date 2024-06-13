using Microsoft.EntityFrameworkCore;
using MyBlogApp.Application.Interfaces;
using MyBlogApp.Domain.Entities;
using MyBlogApp.Infrastructure.Data;

namespace MyBlogApp.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogContext _context;

    public PostRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}