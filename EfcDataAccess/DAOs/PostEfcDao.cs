using Domain.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{

    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
    
        
        if (searchParameters.Title != null)
        {
            query = query.Where(p => p.Title == searchParameters.Title);
        }
    
        if (searchParameters.Body != null)
        {
            query = query.Where(p => p.Body == searchParameters.Body);
        }
        
        if (searchParameters.PostID != null)
        {
            query = query.Where(p => p.Id == searchParameters.PostID);
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }
}