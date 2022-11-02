using Domain.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using FileData;

namespace Domain.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        
        if (searchParams.PostID != null)
        {
            result = result.Where(t => t.Id == searchParams.PostID);
        }

        if (!string.IsNullOrEmpty(searchParams.Title))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.Title, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParams.Body))
        {
            result = result.Where(t =>
                t.Body.Contains(searchParams.Body, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
    
}